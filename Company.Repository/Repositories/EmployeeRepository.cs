﻿using Company.Data.Contexts;
using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Repository.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repositories
{
    public class EmployeeRepository :GenericRepo<Employee> , IEmployeeRepository
    {
        private readonly CompanyDbContext _dbContext;   
        public EmployeeRepository(CompanyDbContext dbContext) :base(dbContext)  
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Employee> GetEmployeeByName(string name)
            => _dbContext.Employees.Where(x => x.Name.Trim().ToLower().Contains(name.Trim().ToLower())
            ||  x.Email.Trim().ToLower().Contains(name.Trim().ToLower())
            ||  x.Address.Trim().ToLower().Contains(name.Trim().ToLower())
            ).ToList();

        public IEnumerable<Employee> GetEmployeesByAddress(string address)
            => _dbContext.Employees.Where(x=> x.Address == address).ToList();

        public IEnumerable<Department> GetDepartments()
            => _dbContext.Departments.ToList();

    }
}
