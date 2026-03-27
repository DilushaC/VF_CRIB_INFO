using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

public class ApiService
{
    public async Task<dynamic> Search(string numberType, string number, bool isIndividual)
    {
        var token = await GetToken();

        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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

        var response = await client.PostAsync(url, content);
        var json = await response.Content.ReadAsStringAsync();

        dynamic obj = JsonConvert.DeserializeObject(json);

        
        return new
        {
            applicationNumber = obj.data.parameters.applicationNumber,
            fullName = obj.data.parameters.fullName,
            creditFacilityType = obj.data.parameters.creditFacilityType,
            creditFacilityAmount = obj.data.parameters.creditFacilityAmount.value
        };
    }



    private async Task<string> GetToken()
    {
        var client = new HttpClient();
        var request = new
        {
            username = "YOUR_USERNAME",
            password = "YOUR_PASSWORD"
        };

        var content = new StringContent(
            JsonConvert.SerializeObject(request),
            Encoding.UTF8,
            "application/json"
        );

        var response = await client.PostAsync("https://identity-url", content);
        var json = await response.Content.ReadAsStringAsync();

        dynamic obj = JsonConvert.DeserializeObject(json);

        return obj.access_token;
    }
}