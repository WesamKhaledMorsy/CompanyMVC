using Company.Data.Contexts;
using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Repository.Interfaces.UnitOfWork;
using Company.Repository.Repositories;
using Company.Service.Interfaces;
using Company.Service.Mapping;
using Company.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();  
            //builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();  
            builder.Services.AddScoped<IDepartmentDtoService,DepartmentService>();
            builder.Services.AddScoped<IEmployeeDtoService,EmployeeService>();

            builder.Services.AddAutoMapper(x=>x.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(x=>x.AddProfile(new DepartmentProfile()));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.Password.RequiredUniqueChars = 2;
                config.Password.RequireDigit = true;
                config.Password.RequireLowercase = true;
                config.Password.RequireUppercase = true;
                config.Password.RequireNonAlphanumeric = true;
                config.Password.RequiredLength= 6;
                config.User.RequireUniqueEmail = true;
                config.Lockout.AllowedForNewUsers = true;
                // 3 trials for write password right
                config.Lockout.MaxFailedAccessAttempts = 3;
                // After 3 trials to write password , try after 60 mins
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
            }).AddEntityFrameworkStores<CompanyDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Receive only http requests , to prevent XSS >> Cross side scripting <<
                options.Cookie.HttpOnly= true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                // to prevent delete the cookie configurations from the site if the user is already Active >> SlidingExpiration << (Reset the Expiration Time with every Request)
                options.SlidingExpiration =true;
                // Redirect to login Page
                options.LoginPath = "/Account/Login";
                options.LogoutPath ="/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.Cookie.Name="Company Cookie";
                options.Cookie.SecurePolicy =CookieSecurePolicy.Always; // to allow only Https sites
                options.Cookie.SameSite = SameSiteMode.Strict; // to prevent Cross side Scripting << prevent taking a cookie from anoher site >>

            }); 


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            //Add Authentiacation
            app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
