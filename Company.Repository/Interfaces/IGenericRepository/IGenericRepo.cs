using Company.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Interfaces.IGenericRepository
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        T GetByID(int id);
        T GetByIDAsNoTracking(int id);
        IQueryable<T> GetAllAsNoTracking();
        IQueryable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
