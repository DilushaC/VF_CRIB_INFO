using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Security;
using System.Text.Json;
using VF_CRIB_CREDITINFO.Business.UserHandler;
using VF_CRIB_CREDITINFO.Presentation.Filters;

namespace VF_CRIB_CREDITINFO.Controllers
{
    [SessionCheck]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private static readonly ILog log =
                LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var user = await _userService.ValidateUserAsync(username, password);

                if (user == null)
                {
                    log.Warn($"Invalid login attempt for: {username}");
                    return Json(new { success = false, message = "Invalid login" });
                }

                log4net.ThreadContext.Properties["user"] = user.DisplayName;
                log4net.ThreadContext.Properties["action"] = "Login";

                // Session storage
                HttpContext.Session.SetString("UserName", user.DisplayName);
                HttpContext.Session.SetString("Designation", user.DisplayDesignation);
                HttpContext.Session.SetString("Department", user.DisplayDepartment);

                // Log AFTER setting context
                log.Info("User logged in successfully");

                return Json(new
                {
                    success = true,
                    redirectUrl = Url.Action("Index", "Home"),
                    loggedUser = user.DisplayName
                });
            }
            catch (Exception ex)
            {
                log.Error("Login failed", ex);
                throw;
            }
        }
    }
}
