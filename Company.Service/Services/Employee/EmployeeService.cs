using Company.Data.Entities;
using Company.Repository.Interfaces.UnitOfWork;
using Company.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public void Add(Employee entity)
        {
            var employee = new Employee
            {
                Name = entity.Name,
                Email = entity.Email,
                Address = entity.Address,
                PhoneNumber = entity.PhoneNumber,
                Age = entity.Age,
                Salary = entity.Salary,
                ImageUrl = entity.ImageUrl,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                DepartmentId = entity.DepartmentId,
                IsDeleted = entity.IsDeleted,
                HiringDate = entity.HiringDate                
            };
            _unitOfWork.EmployeeRepository.Add(employee);
            _unitOfWork.Complete();

        }

        public void Delete(Employee entity)
        {
            if (entity == null)
                throw new Exception("Employee object is null");
            _unitOfWork.EmployeeRepository.Delete(entity);
            _unitOfWork.Complete();
        }

        public IEnumerable<Employee> GetAll()
        {
           var employees =  _unitOfWork.EmployeeRepository.GetAll();
            return employees;
        }
        public IEnumerable<Department> GetDepartments()
        {
            var departments=_unitOfWork.DepartmentRepository.GetAll();
            return departments;
        }
        public Employee GetByID(int? id)
        {
            if (id == null)
                return null;
            var employee = _unitOfWork.EmployeeRepository.GetByID(id.Value);
            if (employee == null)
                return null;
            return employee;

        }

        public Employee GetByIDAsNoTracking(int? id)
        {
            if (id == null)
                return null;
            var employee = _unitOfWork.EmployeeRepository.GetByIDAsNoTracking(id.Value);
            if (employee == null)
                return null;
            return employee;
        }

        public Employee GetEmployeeWithDepartment(int? id)
        {
            if (id == null)
                return null;
            var employee = _unitOfWork.EmployeeRepository.GetByID(id.Value);
            if (employee == null)
                return null;
            return employee;
        }

        public Employee GetEmployeeWithDepartmentAsNoTracking(int? id)
        {
            if (id == null)
                return null;
            var employee = _unitOfWork.EmployeeRepository.GetByID(id.Value);
            if (employee == null)
                return null;
            return employee;
        }

        public void Update(Employee entity)
        {
            var emp = GetByIDAsNoTracking(entity.Id);
            emp.Name = entity.Name;
            emp.Salary = entity.Salary;
            emp.Email = entity.Email;
            emp.Address= entity.Address;
            emp.PhoneNumber = entity.PhoneNumber;
            emp.HiringDate = entity.HiringDate;
            emp.IsDeleted = entity.IsDeleted;

            emp.DepartmentId = entity.DepartmentId;
            _unitOfWork.EmployeeRepository.Update(entity);
            _unitOfWork.Complete();

        }
    }
}
