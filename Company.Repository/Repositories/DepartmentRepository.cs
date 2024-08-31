using Company.Data.Contexts;
using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Repository.Interfaces.IGenericRepository;
using Company.Repository.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repositories
{
    public class DepartmentRepository : GenericRepo<Department> , IDepartmentRepository
    {
       // Write this if here is methods needs dbContext
        private readonly CompanyDbContext _dbContext;
        public DepartmentRepository(CompanyDbContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }
        public Department GetDepartmentWithEmployees(int id)
        {
            var dept = _dbContext.Departments.Where(x=>x.Id == id).Include(x=>x.Employees).FirstOrDefault();
            return dept;
        }

    }
}
