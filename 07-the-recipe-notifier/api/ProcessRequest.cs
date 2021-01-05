using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using RecipeNotifier.Entities;

namespace RecipeNotifier.Api
{
    public static class ProcessRequest
    {
        [FunctionName("ProcessRequest")]
        public static void Run([QueueTrigger("testqueue", Connection = "AzureWebJobsStorage")] RecipeRequest request,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {request.Email}");
        }
    }
}
