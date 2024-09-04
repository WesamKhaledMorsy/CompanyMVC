using Company.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Interfaces
{
    public interface IEmployeeService
    {
        Employee GetByID(int? id);
        Employee GetByIDAsNoTracking(int? id);
        IEnumerable<Employee> GetAll();
        void Add(Employee entity);
        void Update(Employee entity);
        void Delete(Employee entity);
        Employee GetEmployeeWithDepartment(int? id);
        Employee GetEmployeeWithDepartmentAsNoTracking(int? id);
    }
}
