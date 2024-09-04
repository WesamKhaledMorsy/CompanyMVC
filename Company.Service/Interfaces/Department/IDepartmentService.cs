using Company.Data.Contexts.Configurations;
using Company.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Interfaces
{
    public interface IDepartmentService
    {
        Department GetByID(int? id);
        Department GetByIDAsNoTracking(int? id);
        IQueryable<Department> GetAll();
        void Add(Department entity);
        void Update(Department entity);
        void Delete(Department entity);
        Department GetDepartmentWithEmployees(int? id);
        Department GetDepartmentWithEmployeesAsNoTracking(int? id);
    }
}
