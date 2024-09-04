using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        //Add All Repositories
        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        int Complete();
    }
}
