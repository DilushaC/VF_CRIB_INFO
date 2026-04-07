using Microsoft.AspNetCore.Mvc;
using VF_CRIB_CREDITINFO.Business.CRIBHandler;
using VF_CRIB_CREDITINFO.Business.SearchCRIBHandler;

namespace VF_CRIB_CREDITINFO.Presentation.Controllers
{
    public class CRIBController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly  ICRIBService _CRIBService;

        public CRIBController(ITokenService tokenService,ICRIBService CRIBService)
        {
            _tokenService = tokenService;
            _CRIBService = CRIBService;
        }

        [HttpGet]
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


        [HttpPost]
        public async Task<JsonResult> SearchData(string numberType, string number, bool isIndividual)
        {
            string token = "TOKEN_Number";

            var result = await _CRIBService.SearchCRIBData(
                numberType,
                number,
                isIndividual,
                token
            );

            return Json(result);
        }
    }
}
