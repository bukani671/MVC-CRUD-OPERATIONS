using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task <IActionResult> Index()
        {
          var employees =  await mVCCrudDbContext.Employees.ToListAsync();
            return View(employees);
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

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mVCCrudDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null) {
                var viewModel = new UpdateEmployeeViewModel()
                {

                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,

                };
                return await Task.Run(()=>View("View",viewModel));
            }

            return RedirectToAction("Index");
            
           
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mVCCrudDbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await mVCCrudDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("index");
            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel deleteModel)
        {
            var employee =  await mVCCrudDbContext.Employees.FindAsync(deleteModel.Id);

            if (employee != null)
            {
                mVCCrudDbContext.Employees.Remove(employee);
                await mVCCrudDbContext.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
    }
}
