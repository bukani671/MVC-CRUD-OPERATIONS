using Microsoft.AspNetCore.Mvc;
using MVC_CRUD_APP.Data;
using MVC_CRUD_APP.Models;
using MVC_CRUD_APP.Models.Domain;

namespace MVC_CRUD_APP.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MVCCrudDbContext mVCCrudDbContext;
        public EmployeeController(MVCCrudDbContext mVCCrudDbContext)
        {
            this.mVCCrudDbContext = mVCCrudDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth,

            };
         await mVCCrudDbContext.Employees.AddAsync(employee);
         await mVCCrudDbContext.SaveChangesAsync();
            return RedirectToAction("Add");

        }

    }
}
