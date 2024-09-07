using Company.Data.Contexts.Configurations;
using Company.Data.Entities;
using Company.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Interfaces
{
    public interface IDepartmentDtoService
    {
        DepartmentDto GetByID(int? id);
        DepartmentDto GetByIDAsNoTracking(int? id);
        IEnumerable<DepartmentDto> GetAll();
        void Add(DepartmentDto entity);
        void Update(DepartmentDto entity);
        void Delete(DepartmentDto entity);
        DepartmentDto GetDepartmentDtoWithEmployees(int? id);
        DepartmentDto GetDepartmentDtoWithEmployeesAsNoTracking(int? id);
    }
}
