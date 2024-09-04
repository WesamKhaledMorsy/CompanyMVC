using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data.Entities
{
    public class Department :BaseEntity
    {
        public Department() { }
        [Required(ErrorMessage="Department Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Department Code is Required")]
        public string Code { get; set; }
        public ICollection<Employee>? Employees { get; set; }    

    }
}
