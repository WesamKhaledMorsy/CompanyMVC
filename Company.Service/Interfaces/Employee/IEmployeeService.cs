using Company.Data.Entities;
using Company.Service.Services.Employee.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Interfaces
{
    public interface IEmployeeDtoService
    {
        EmployeeDto GetByID(int? id);
        EmployeeDto GetByIDAsNoTracking(int? id);
        IEnumerable<EmployeeDto> GetEmployeeDtoByName (string name);
        IEnumerable<EmployeeDto> GetAll();
        void Add(EmployeeDto entity);
        void Update(EmployeeDto entity);
        void Delete(EmployeeDto entity);
        //EmployeeDto GetEmployeeDtoWithDepartment(int? id);
        //EmployeeDto GetEmployeeDtoWithDepartmentAsNoTracking(int? id);
    }
}
