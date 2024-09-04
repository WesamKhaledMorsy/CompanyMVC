using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Repository.Repositories;
using Company.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger) 
        { 
              _departmentService = departmentService;
            _logger=logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var departments= _departmentService.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Details(int? id, string viewname="Details")
        {
            try
            {                
                var depertment = _departmentService.GetDepartmentWithEmployees(id);
                if(depertment == null)
                    return RedirectToAction("NotFoundPage","Home");
                return View(viewname,depertment);
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
            return View(new Department());
        }
        [HttpPost]
        public IActionResult Create(Department  model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _departmentService.Add(model);
                    //return RedirectToAction("Index");
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Department Error", "Validation Errors");
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Department Error", ex.Message);
                return View(model);
            }
           

        }
        [HttpGet ]
        public IActionResult Update(int id)
        {

            //if (id == null)
            //    return BadRequest();
            //var depertment = _departmentService.GetDepartmentWithEmployees(id);
            //if (depertment == null)
            //    return RedirectToAction("NotFoundPage", "Home");
            //return View(depertment);
            return Details(id,"Update");
        }

        [HttpPost]
        public IActionResult Update(int id,Department departmentModel)
        {
            try
            {
                if (id != departmentModel.Id)
                    return RedirectToAction("NotFoundPage", "Home");
                if (ModelState.IsValid)
                {                    
                    _departmentService.Update(departmentModel);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message);
            }      
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("NotFoundPage", "Home");
            var depertment = _departmentService.GetDepartmentWithEmployees(id);
            if (depertment == null)
                return RedirectToAction("NotFoundPage", "Home");

            _departmentService.Delete(depertment);
            return RedirectToAction("Index");
        }
    }
}
