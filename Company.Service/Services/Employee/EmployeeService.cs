using AutoMapper;
using Company.Data.Entities;
using Company.Repository.Interfaces.UnitOfWork;
using Company.Service.Helper;
using Company.Service.Interfaces;
using Company.Service.Services.Employee.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class EmployeeService : IEmployeeDtoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }
        public void Add(EmployeeDto entity)
        {
            entity.ImageUrl = DocumentSettings.UploadFile(entity.Image, "Images");
            Data.Entities.Employee employee  = _mapper.Map<Data.Entities.Employee>(entity);
            _unitOfWork.EmployeeRepository.Add(employee);
            _unitOfWork.Complete();

        }

        public void Delete(EmployeeDto entity)
        {
            if (entity == null)
                throw new Exception("Employee object is null");
            Data.Entities.Employee employee = _mapper.Map<Data.Entities.Employee>(entity);
            _unitOfWork.EmployeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
           var employees =  _unitOfWork.EmployeeRepository.GetAll();          
            IEnumerable<EmployeeDto> mappedEmployees= _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return mappedEmployees;
        }
        public IEnumerable<DepartmentDto> GetDepartments()
        {
            var departments=_unitOfWork.DepartmentRepository.GetAll();
            IEnumerable<DepartmentDto> department = _mapper.Map<IEnumerable<DepartmentDto>>(departments);

            return department;
        }
        public EmployeeDto GetByID(int? id)
        {
            if (id == null)
                return null;
            var employee = _unitOfWork.EmployeeRepository.GetByID(id.Value);
            if (employee == null)
                return null;         
            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public EmployeeDto GetByIDAsNoTracking(int? id)
        {
            if (id == null)
                return null;
            var employee = _unitOfWork.EmployeeRepository.GetByIDAsNoTracking(id.Value);
            if (employee == null)
                  return null;            
           EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        //public EmployeeDto GetEmployeeWithDepartment(int? id)
        //{
        //    if (id == null)
        //        return null;
        //    var employee = _unitOfWork.EmployeeRepository.GetByID(id.Value);
        //    if (employee == null)
        //        return null;          
        //    EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);
        //    return employeeDto;
        //}

        //public EmployeeDto GetEmployeeWithDepartmentAsNoTracking(int? id)
        //{
        //    if (id == null)
        //        return null;
        //    var employee = _unitOfWork.EmployeeRepository.GetByID(id.Value);
        //    if (employee == null)
        //        return null;          
        //    EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);
        //    return employeeDto;
        //}

        public void Update(EmployeeDto entity)
        {
            var emp = GetByIDAsNoTracking(entity.Id);
            entity.ImageUrl= DocumentSettings.UploadFile(entity.Image, "Images");
            Data.Entities.Employee employeeDto = _mapper.Map<Data.Entities.Employee>(emp);
            _unitOfWork.EmployeeRepository.Update(employeeDto);
            _unitOfWork.Complete();

        }
        public IEnumerable<EmployeeDto> GetEmployeeDtoByName(string name)
        { 
            var employees =  _unitOfWork.EmployeeRepository.GetEmployeeByName(name);          
            IEnumerable<EmployeeDto> mappedEmployees = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return mappedEmployees;
        }
    }
}
