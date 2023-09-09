using Microsoft.AspNetCore.Mvc;

namespace MVC_CRUD_APP.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


    }
}
