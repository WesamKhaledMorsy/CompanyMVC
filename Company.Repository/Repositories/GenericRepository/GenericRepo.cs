using Company.Data.Contexts;
using Company.Data.Entities;
using Company.Repository.Interfaces.IGenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repositories.GenericRepository
{
    public class GenericRepo<T> : IGenericRepo<T> where T: BaseEntity   
    {
        private readonly CompanyDbContext _dbContext;
        public GenericRepo(CompanyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
            => _dbContext.Set<T>().Add(entity);
      
        public void Delete(T entity)
            =>_dbContext.Set<T>().Remove(entity);

        public IQueryable<T> GetAll()
            => _dbContext.Set<T>().AsQueryable();
        public IQueryable<T> GetAllAsNoTracking()
         => _dbContext.Set<T>().AsNoTracking().AsQueryable();

        public T GetByID(int id)
             =>_dbContext.Set<T>().FirstOrDefault(x => x.Id == id) ?? throw new Exception("Id Not Found");

        public T GetByIDAsNoTracking(int id)
             => _dbContext.Set<T>().AsNoTracking().FirstOrDefault(x => x.Id == id) ?? throw new Exception("Id Not Found");

        public void Update(T entity)
            => _dbContext.Set<T>().Update(entity);        
        

    }
}
