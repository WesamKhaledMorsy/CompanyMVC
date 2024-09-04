using Company.Data.Entities;
using Company.Repository.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Interfaces
{
    public interface IDepartmentRepository:IGenericRepo<Department>
    {
        Department GetDepartmentWithEmployees(int ?id);
        Department GetDepartmentWithEmployeesAsNoTracking(int? id);
    }
}
