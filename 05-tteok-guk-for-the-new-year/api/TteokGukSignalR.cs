using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.AspNetCore.Http;
using TteokGuk.Api.Entities;

namespace TteokGuk.Api
{
    public static class Functions
    {
        // This will manage connections to SignalR
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo GetSignalRInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "tteokGukHub")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

        //Send the messages!
        [FunctionName("messages")]
        public static Task SendMessage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] TteokGukMessage tteokGukMessage,
            [SignalR(HubName = "tteokGukHub")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "tteokGukMessage",
                    Arguments = new[] { tteokGukMessage }
                });
        }
    }
}