using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace RecipeNotifier.Api
{
    public static class AzureMaps
    {
        [FunctionName("AzureMaps")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "azuremaps")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string responseMessage = Environment.GetEnvironmentVariable("AzureMapsSubscriptionKey");

            return new OkObjectResult(responseMessage);
        }
    }
}
