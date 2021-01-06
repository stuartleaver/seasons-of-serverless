using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using RecipeNotifier.Entities;

namespace RecipeNotifier.Api
{
    public static class GetRegionalCenter
    {
        private static HttpClient _httpClient = new HttpClient();

        [FunctionName("GetRegionalCenter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getregionalcenter/{latitude}/{longitude}")] HttpRequest req,
            string latitude,
            string longitude,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var azureMapsSubscriptionKey = Environment.GetEnvironmentVariable("AzureMapsSubscriptionKey");

            var url = $"https://atlas.microsoft.com/search/address/reverse/json?api-version=1.0&subscription-key={azureMapsSubscriptionKey}&language=en-US&query={latitude},{longitude}&number=1";

            var response = await _httpClient.GetAsync(url);
            var result = response.Content.ReadAsStringAsync().Result;
            dynamic data = JsonConvert.DeserializeObject(result);

            var regionalCenterInformation = new RegionalCenterInformation() {
                CountrySubdivision = data.addresses[0].address.countrySubdivision,
                Municipality = data.addresses[0].address.municipality,
                Country = data.addresses[0].address.country
            };

            return new OkObjectResult(regionalCenterInformation);
        }
    }
}
