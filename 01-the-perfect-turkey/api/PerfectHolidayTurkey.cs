using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace PerfectHolidayTurkey.Api
{
    public static class HttpTriggerCSharp1
    {
        [FunctionName("PerfectHolidayTurkey")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "perfectholidayturkey")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            string turkeyWeight = req.Query["turkeyWeight"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            turkeyWeight = turkeyWeight ?? data?.turkeyWeight;

            log.LogInformation($"Turkey weight (lbs): {turkeyWeight}");

            var brineInstructions = new Recipe
            {
                TurkeyWeight = Convert.ToDecimal(turkeyWeight)
            };

            return new OkObjectResult(JsonConvert.SerializeObject(brineInstructions));
        }
    }
}
