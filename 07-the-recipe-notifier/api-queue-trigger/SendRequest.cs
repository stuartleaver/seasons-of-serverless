using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecipeNotifier.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RecipeNotifier.Api
{
    public static class SendRequest
    {
        [FunctionName("SendRequest")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "sendrequest")] HttpRequest req,
            [Queue("recipeconnectorqueue"),StorageAccount("AzureWebJobsStorage")] ICollector<RecipeRequest> queue,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<RecipeRequest>(requestBody);

            queue.Add(data);

            return new OkObjectResult("Request recieved");
        }
    }
}
