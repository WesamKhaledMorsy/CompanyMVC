using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository ) 
        { 
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            var departments= _departmentRepository.GetAll();
            return View(departments);
        }
        public IActionResult Details(int id)
        {
            var depertment = _departmentRepository.GetDepartmentWithEmployees(id);
            return View(depertment);
        }
        public IActionResult Update(int id)
        {

            var depertment = _departmentRepository.GetByID(id);
            _departmentRepository.Update(depertment);
            return View(depertment);
        }
        public IActionResult Delete(int id)
        {
            var depertment = _departmentRepository.GetByID(id);
            _departmentRepository.Delete(depertment);
            return View(depertment);
        }
    }
}
