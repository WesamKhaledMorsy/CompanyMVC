using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Repository.Interfaces.UnitOfWork;
using Company.Service.Interfaces;
using Company.Service.Services;
using Company.Service.Services.Employee.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Company.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeDtoService _employeeService;
        private readonly IDepartmentDtoService _departmentService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IEmployeeDtoService employeeService, ILogger<EmployeeController> logger, IDepartmentDtoService departmentService, IUnitOfWork unitOfWork)
        {
            _employeeService = employeeService;
            _logger=logger;
            _departmentService=departmentService;
            _unitOfWork=unitOfWork;
        }
        public IActionResult Index(string? searchInput)
        {
            IEnumerable<EmployeeDto> employees = new List <EmployeeDto>();
            if (string.IsNullOrEmpty(searchInput))
                 employees = _employeeService.GetAll();
            else
                employees = _employeeService.GetEmployeeDtoByName(searchInput);
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
            ViewBag.Departments= _departmentService.GetAll();           
            return View(new EmployeeDto());
        }
        [HttpPost]
        //   This attribute helps defend against cross-site request forgery. It won't prevent
        //   other forgery or tampering attacks.
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeDto model)
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
            ViewBag.Departments = _departmentService.GetAll();
            return Details(id, "Update");
        }

        [HttpPost]
        public IActionResult Update(int id, EmployeeDto employeeModel)
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
