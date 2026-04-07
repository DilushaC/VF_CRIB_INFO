using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF_CRIB_CREDITINFO.Business.CRIBHandler
{
    using System.Net.Http;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class TokenService : ITokenService
    {
        private readonly HttpClient _httpClient;

        public TokenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetTokenDataAsync(string url, string username, string password)
        {
            var data = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "scope", "cb5webservices" },
                { "client_id", username },
                { "client_secret", password }
            };

            using var response = await _httpClient.PostAsync(
                url,
                new FormUrlEncodedContent(data)
            );

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
