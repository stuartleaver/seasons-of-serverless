using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace TteokGuk.Api
{
    public static class SoakGaraeTteok
    {
        [FunctionName("SoakGaraeTteok")]
        public static async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var timer = context.CurrentUtcDateTime.Add(TimeSpan.FromMinutes(1));

            await context.CreateTimer(timer, CancellationToken.None);

            await context.CallActivityAsync("SoakGaraeTteok_Completed", "SoakGaraeTteok");
        }

        [FunctionName("SoakGaraeTteok_Completed")]
        public static string SoakGaraeTteokCompleted([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Saying hello to {name}.");
            return $"Hello {name}!";
        }

        [FunctionName("SoakGaraeTteok_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("SoakGaraeTteok", null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}