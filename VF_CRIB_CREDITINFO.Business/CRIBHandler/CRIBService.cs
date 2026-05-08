using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using VF_CRIB_CREDITINFO.Business.ConnectionHandler;
using VF_CRIB_CREDITINFO.Data.Context;
using VF_CRIB_CREDITINFO.Data.Models;

namespace VF_CRIB_CREDITINFO.Business.SearchCRIBHandler
{
    public class CRIBService : ICRIBService
    {
        private readonly HttpClient _httpClient;
        private readonly _ConnectionService _connectionService;

        public CRIBService(HttpClient httpClient, _ConnectionService connectionService)
        {
            _httpClient = httpClient;
            _connectionService = connectionService;
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

        public IEnumerable<CurrencyModel> GetActiveCurrencies()
        {
            var query = "SELECT Id, Currency, IsActive FROM CreditFacilityCurrency WHERE IsActive = 1";
            return _connectionService.Query<CurrencyModel>(query);
        }

        public IEnumerable<CreditFacilityTypeModel> GetCreditFacilityTypes()
        {
            var query = "SELECT Id, FacilityType, IsActive FROM CreditFacilityType WHERE IsActive = 1";
            return _connectionService.Query<CreditFacilityTypeModel>(query);
        }

        public IEnumerable<InquiryReasonModel> GetInquiryReasons()
        {
            var query = "SELECT Id, InquiryReason, IsActive FROM InquiryReason WHERE IsActive = 1";
            return _connectionService.Query<InquiryReasonModel>(query);
        }

        // FIXED: replaced undefined connStr + raw SqlCommand with _connectionService + Dapper,
        // consistent with all other methods in this class
        public async Task<CribSearchResultModel> GetSearchResultAsync(string applicationNumber)
        {
            var query = @"
                SELECT TOP 1
                    Id, RequestId, WorkflowId, WorkflowState, Status,
                    ApplicationNumber, CreditFacilityType, CreditFacilityCurrency,
                    CreditFacilityAmount, FullName, Gender, DateOfBirth,
                    NicNumber, AltNicNumber, AddressLine, City, Country,
                    DataAvailabilityJson, CreatedAt
                FROM CribSearchResults
                WHERE ApplicationNumber = @AppNo
                ORDER BY CreatedAt DESC";

            return await _connectionService.QueryFirstOrDefaultAsync<CribSearchResultModel>(
                query,
                new { AppNo = applicationNumber ?? "" }
            );
        }
    }
}