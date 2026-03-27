using Microsoft.AspNetCore.Mvc;
using VF_CRIB_CREDITINFO.Business.SearchCRIBHandler;

namespace VF_CRIB_CREDITINFO.Presentation.Controllers
{
    public class CRIBController : Controller
    {
        private readonly ISearchCRIBService _SearchCRIBService;

        public CRIBController(ISearchCRIBService SearchCRIBService)
        {
            _SearchCRIBService = SearchCRIBService;
        }

        public IActionResult SearchCRIB()
        {
            return View();
        }       
      

        [HttpPost]
        public async Task<JsonResult> SearchData(string numberType, string number, bool isIndividual)
        {
            string token = "TOKEN_Number"; 

            var result = await _SearchCRIBService.SearchCRIBData(
                numberType,
                number,
                isIndividual,
                token
            );

            return Json(result);
        }

    }
}
