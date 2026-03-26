using Microsoft.AspNetCore.Mvc;

namespace VF_CRIB_CREDITINFO.Presentation.Controllers
{
    public class SearchCRIBController : Controller
    {

        public IActionResult SearchCRIB()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SearchData(string numberType, string number, bool isIndividual)
        {
            var service = new ApiService();

            var result = await service.Search(numberType, number, isIndividual);

            return Json(result);
        }


    }
}
