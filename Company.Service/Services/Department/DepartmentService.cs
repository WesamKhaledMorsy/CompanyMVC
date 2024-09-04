using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Repository.Interfaces.UnitOfWork;
using Company.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentService( IUnitOfWork unitOfWork)
        {       
            _unitOfWork=unitOfWork;
        }


        public void Add(Department entity)
        {
            //Mapping
            var mappedDepartemnt = new Department 
            {
                Code =  entity.Code ,
                Name = entity.Name ,
                Employees = entity.Employees ,
                CreatedAt = DateTime.Now 
            };
            _unitOfWork.DepartmentRepository.Add(mappedDepartemnt);
            _unitOfWork.Complete();

        }

        public void Delete(Department entity)
        {
            if (entity.Employees.Count()>0)
                throw new Exception("You can not delete the department tht has Employees in it");
            _unitOfWork.DepartmentRepository.Delete(entity);
            _unitOfWork.Complete();
        }

        public IQueryable<Department> GetAll()
        {
            var departments= _unitOfWork.DepartmentRepository.GetAll();
            return departments;
        }

        public Department GetByID(int? id)
        {
            if(id is null)
                //throw new Exception("Id IS Null");
                return null;
            var depertment = _unitOfWork.DepartmentRepository.GetByID(id.Value);
            if (depertment is null)
                return null;
            return depertment;
        }
        public Department GetByIDAsNoTracking(int? id)
        {
            if (id is null)
                //throw new Exception("Id IS Null");
                return null;
            var depertment = _unitOfWork.DepartmentRepository.GetByIDAsNoTracking(id.Value);
            if (depertment is null)
                return null;
            return depertment;
        }

        public void Update(Department entity)
        {
            //if (entity is null)
            //    return ;
            //_departmentRepository.Update(entity);
            //return entity;

            ///OR
            //_departmentRepository.Update(entity);

            ///OR 
            var oldDept = GetByID(entity.Id);
          
                //if(GetAll().Any(x=>x.Name == entity.Name))
                //{
                //    throw new Exception("DuplicateDepartmentName");
                //}
                oldDept.Name= entity.Name;
                oldDept.Code= entity.Code;
                _unitOfWork.DepartmentRepository.Update(oldDept);
                _unitOfWork.Complete();
            
        }
        public Department GetDepartmentWithEmployees(int? id)
        {
            if (id is null)
                //throw new Exception("Id IS Null");
                return null;
            var depertment = _unitOfWork.DepartmentRepository.GetDepartmentWithEmployees(id.Value);
            if (depertment is null)
                return null;
            return depertment;
        }
        public Department GetDepartmentWithEmployeesAsNoTracking(int? id)
        {
            if (id is null)
                //throw new Exception("Id IS Null");
                return null;
            var depertment = _unitOfWork.DepartmentRepository.GetDepartmentWithEmployeesAsNoTracking(id.Value);
            if (depertment is null)
                return null;
            return depertment;
        }
    }
}
