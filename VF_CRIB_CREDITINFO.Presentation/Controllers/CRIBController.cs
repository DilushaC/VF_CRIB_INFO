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

        public IActionResult Index()
        {
            ViewBag.Currencies = _CRIBService.GetActiveCurrencies();
            ViewBag.CreditFacilityTypes = _CRIBService.GetCreditFacilityTypes();
            ViewBag.InquiryReasons = _CRIBService.GetInquiryReasons();
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
