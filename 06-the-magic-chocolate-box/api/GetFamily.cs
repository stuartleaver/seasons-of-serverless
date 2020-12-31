using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using MagicChocolateBox.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace MagicChocolateBox.Api
{
    public static class GetFamily
    {
        [FunctionName("GetFamily")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "getfamily")] HttpRequest req,
            [DurableClient] IDurableEntityClient client)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string familyName = data?.familyName;

            var entityId = new EntityId(nameof(Family), familyName);

            var stateResponse = await client.ReadEntityStateAsync<Family>(entityId);

            return new OkObjectResult(stateResponse.EntityState);
        }
    }
}
