using AutoMapper;
using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Repository.Interfaces.UnitOfWork;
using Company.Service.Interfaces;
using Company.Service.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class DepartmentService : IDepartmentDtoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepartmentService( IUnitOfWork unitOfWork,IMapper mapper)
        {       
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }


        public void Add(DepartmentDto entity)
        {
            //Mapping
            Department mappedDepartemnt = _mapper.Map<Department>(entity);
            _unitOfWork.DepartmentRepository.Add(mappedDepartemnt);
            _unitOfWork.Complete();

        }

        public void Delete(DepartmentDto entity)
        {
            if (entity.Employees.Count()>0)
                throw new Exception("You can not delete the department tht has Employees in it");
            Department mappedDepartemnt = _mapper.Map<Department>(entity);
            _unitOfWork.DepartmentRepository.Delete(mappedDepartemnt);
            _unitOfWork.Complete();
        }

        public IEnumerable<DepartmentDto> GetAll()
        {
            var departments= _unitOfWork.DepartmentRepository.GetAll();
            IEnumerable<DepartmentDto> mappedDepartemnts = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return mappedDepartemnts;
        }

        public DepartmentDto GetByID(int? id)
        {
            if(id is null)
                //throw new Exception("Id IS Null");
                return null;
            var department = _unitOfWork.DepartmentRepository.GetByID(id.Value);
            var employees = _unitOfWork.EmployeeRepository.GetAll().Where(x => x.DepartmentId == id.Value).ToList();
            department.Employees =employees;
            if (department is null)
                return null;
         
            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
        }
        public DepartmentDto GetByIDAsNoTracking(int? id)
        {
            if (id is null)
                //throw new Exception("Id IS Null");
                return null;
            var department = _unitOfWork.DepartmentRepository.GetByIDAsNoTracking(id.Value);
            var employees = _unitOfWork.EmployeeRepository.GetAll().Where(x=>x.DepartmentId == id.Value).ToList();
            department.Employees =employees;
            if (department is null)
                return null;
            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
        }

        public void Update(DepartmentDto entity)
        {
            //if (entity is null)
            //    return ;
            //_departmentRepository.Update(entity);
            //return entity;

            ///OR
            //_departmentRepository.Update(entity);

            ///OR 
            var oldDept = GetByIDAsNoTracking(entity.Id);
          
                //if(GetAll().Any(x=>x.Name == entity.Name))
                //{
                //    throw new Exception("DuplicateDepartmentName");
                //}
                oldDept.Name= entity.Name;
                oldDept.Code= entity.Code;
            oldDept.Employees = entity.Employees;
            
            Department department = _mapper.Map<Department>(oldDept);
            _unitOfWork.DepartmentRepository.Update(department);
                _unitOfWork.Complete();
            
        }
        public DepartmentDto GetDepartmentDtoWithEmployees(int? id)
        {
            if (id is null)
                //throw new Exception("Id IS Null");
                return null;
            var department = _unitOfWork.DepartmentRepository.GetDepartmentWithEmployees(id.Value);
            if (department is null)
                return null;
            DepartmentDto departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
        }
        public DepartmentDto GetDepartmentDtoWithEmployeesAsNoTracking(int? id)
        {
            if (id is null)
                //throw new Exception("Id IS Null");
                return null;
            var department = _unitOfWork.DepartmentRepository.GetDepartmentWithEmployeesAsNoTracking(id.Value);
            if (department is null)
                return null;
            DepartmentDto departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
        }
    }
}
