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

        [HttpPost]
        public async Task<IActionResult> SearchData(string numberType, string number,
            string fullName, string gender, string companyName, bool consentGiven,
            bool isIndividual, string inquiryReason, bool interactiveSearch,
            string applicationNumber)
        {
            var result = await _CRIBService.GetSearchResultAsync(applicationNumber);

            if (result == null)
            {
                return Json(new { success = false, message = "No record found for the given application number." });
            }

            return Json(new
            {
                success = true,
                requestId = result.RequestId,
                workflowId = result.WorkflowId,
                workflowState = result.WorkflowState,
                status = result.Status,
                applicationNumber = result.ApplicationNumber,
                creditFacilityType = result.CreditFacilityType,
                creditFacilityCurrency = result.CreditFacilityCurrency,
                creditFacilityAmount = result.CreditFacilityAmount.ToString("N2"),
                fullName = result.FullName,
                gender = result.Gender,
                dateOfBirth = result.DateOfBirth.HasValue
                                            ? result.DateOfBirth.Value.ToString("yyyy-MM-dd") : "",
                nicNumber = result.NicNumber,
                altNicNumber = result.AltNicNumber,
                addressLine = result.AddressLine,
                city = result.City,
                country = result.Country,
                dataAvailability = result.DataAvailabilityJson,
                reportDate = result.CreatedAt.ToString("yyyy-MM-dd HH:mm")
            });
        }
    }
}
