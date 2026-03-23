using Microsoft.AspNetCore.Mvc;
using System.Security;
using System.Text.Json;

namespace VF_CRIB_CREDITINFO.Controllers
{
    //[SessionCheck]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            //HttpContext.Session.Clear();
            return View();
        }
    }
}
