using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Repository.Interfaces.UnitOfWork;
using Company.Service.Interfaces;
using Company.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _logger=logger;
            _departmentService=departmentService;
        }
        public IActionResult Index()
        {
            var employees = _employeeService.GetAll();
            return View(employees);
        }
        //public IActionResult Details(int id)
        //{
        //    var employee = _employeeService.GetByID(id);
        //    return View(employee);
        //}
        [HttpGet]
        public IActionResult Details(int? id, string viewname = "Details")
        {
            try
            {
                var employee = _employeeService.GetByIDAsNoTracking(id);
                if (employee == null)
                    return RedirectToAction("NotFoundPage", "Home");
                return View(viewname, employee);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return RedirectToAction("NotFoundPage", "Home");
            }
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            List<Department> departments = _departmentService.GetAll().ToList();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            return View(new Employee());
        }
        [HttpPost]
        public IActionResult Create(Employee model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.Add(model);
                    //return RedirectToAction("Index");
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Employee Error", "Validation Errors");
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Employee Error", ex.Message);
                return View(model);
            }


        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            List<Department> departments = _departmentService.GetAll().ToList();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            return Details(id, "Update");
        }

        [HttpPost]
        public IActionResult Update(int id, Employee employeeModel)
        {
            try
            {
                if (id != employeeModel.Id)
                    return RedirectToAction("NotFoundPage", "Home");
                if (ModelState.IsValid)
                {
                    _employeeService.Update(employeeModel);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("NotFoundPage", "Home");
            var depertment = _employeeService.GetByID(id);
            if (depertment == null)
                return RedirectToAction("NotFoundPage", "Home");

            _employeeService.Delete(depertment);
            return RedirectToAction("Index");
        }

    }
}
