using Microsoft.AspNetCore.Mvc;
using VF_CRIB_CREDITINFO.Business.CRIBHandler;

namespace VF_CRIB_CREDITINFO.Presentation.Controllers
{
    public class CRIBController : Controller
    {
        private readonly ITokenService _tokenService;

        public CRIBController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<IActionResult> Index()
        {
            const string serviceURL = "https://identity.cbsnext.domain/connect/token/";
            string username = "USERNAME";
            string password = "PASSWORD";

            var tokenData = await _tokenService.GetTokenDataAsync(
                serviceURL,
                username,
                password
            );

            ViewBag.Token = tokenData;

            return View();
        }
    }
}
