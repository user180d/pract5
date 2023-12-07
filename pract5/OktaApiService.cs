using Newtonsoft.Json.Linq;

namespace pract5
{
    public class OktaApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string apiUrl = "https://dev-29574155.okta.com/api/v1/users?q=";
        private readonly string token = "SSWS "+"00Z5MuUbhdmm21taSOIoMOuZk9H0pJ2EhiL2aIAKWG";
        public  OktaApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization",token);
        }

        public async Task<string> GetProfile(string id)
        {
            string url = $"{apiUrl}{id}&limit=1";
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            string body = await response.Content.ReadAsStringAsync();
            return body;
        }
    }
}
