using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecipeNotifier.Entities;

namespace RecipeNotifier.Api
{
    public static class SendRequest
    {
        [FunctionName("SendRequest")]
        public static async void Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "sendrequest")] HttpRequest req,
            [Queue("testqueue"),StorageAccount("AzureWebJobsStorage")] ICollector<RecipeRequest> queue,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<RecipeRequest>(requestBody);

            queue.Add(data);
        }
    }
}
