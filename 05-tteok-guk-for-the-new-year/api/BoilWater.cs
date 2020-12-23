using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using TteokGuk.Api.Entities;

namespace TteokGuk.Api
{
    public static class BoilWater
    {
        [FunctionName("BoilWater")]
        public static async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var data = context.GetInput<TteokGukMessage>();

            var timer = context.CurrentUtcDateTime.Add(TimeSpan.FromMinutes(data.Duration));
            
            await context.CreateTimer(timer, CancellationToken.None);

            await context.CallActivityAsync("BoilWater_Completed", data);
        }

        [FunctionName("BoilWater_Completed")]
        public static Task BoilWaterCompleted([ActivityTrigger] TteokGukMessage tteokGukMessage,
        [SendGrid(ApiKey = "SENDGRID_API_KEY")] out SendGridMessage message,
        [SignalR(HubName = "tteokGukHub")] IAsyncCollector<SignalRMessage> signalRMessages,
        ILogger log)
        {
            log.LogInformation($"BoilWater_Completed: {tteokGukMessage.UniqueTteokGukInstructionsId}.");

            message = new SendGridMessage();
            message.AddTo(tteokGukMessage.EmailAddress);
            message.AddContent("text/html", "This is to remind you that your water has now boiled and you can now view the rest of the instructions. ");
            message.SetFrom(new EmailAddress("noreply@tteokguk.cloud"));
            message.SetSubject("Your Tteok Guk Reminder - Water has boiled");

            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "tteokGukMessage",
                    Arguments = new[] {
                        new TteokGukMessage
                        {
                            UniqueTteokGukInstructionsId = tteokGukMessage.UniqueTteokGukInstructionsId,
                            Message = "BoilWaterCompleted"
                        }
                    }
                });
        }

        [FunctionName("BoilWater_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "BoilWater")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            var data = await req.Content.ReadAsAsync<TteokGukMessage>();

            string instanceId = await starter.StartNewAsync("BoilWater", data);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}