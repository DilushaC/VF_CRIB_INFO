using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VF_CRIB_CREDITINFO.Business.SearchCRIBHandler
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class SearchCRIBService : ISearchCRIBService
    {
        private readonly HttpClient _httpClient;

        public SearchCRIBService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SearchCRIBData(string numberType, string number, bool isIndividual, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var requestBody = new
            {
                parameters = new
                {
                    fullName = "",
                    gender = "",
                    idNumbersList = new[]
                    {
                new {
                    idNumberType = numberType == "NIC" ? "NIC" : "PassportNumber",
                    idNumber = number
                }
            }
                },
                inquiryReason = "ReviewAsAGuarantorForANewCreditFacility",
                interactiveSearch = false,
                consent = true
            };

            var url = isIndividual
                ? "https://api-url/search/smart/individual"
                : "https://api-url/search/smart/company";

            var content = new StringContent(
                JsonConvert.SerializeObject(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(url, content);

            return await response.Content.ReadAsStringAsync();
        }


    }

}
