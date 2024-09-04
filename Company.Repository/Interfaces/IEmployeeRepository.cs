using Company.Data.Entities;
using Company.Repository.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Interfaces
{
    public interface IEmployeeRepository :IGenericRepo<Employee>
    {
        Employee GetEmployeeByName(string name);
        IEnumerable<Employee> GetEmployeesByAddress(string address);
        IEnumerable<Department> GetDepartments();
    }
}
