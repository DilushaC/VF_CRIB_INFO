using Microsoft.AspNetCore.Mvc;
using VF_CRIB_CREDITINFO.Business.CRIBHandler;
using VF_CRIB_CREDITINFO.Business.SearchCRIBHandler;

namespace VF_CRIB_CREDITINFO.Presentation.Controllers
{
    public class CRIBController : Controller
    {

        {
        }

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

                numberType,
                number,
                isIndividual,
                token
            );

            return Json(result);
        }

    }
}
