using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using MagicChocolateBox.Api.Entities;
using MagicChocolateBox.Dto;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace MagicChocolateBox.Api
{
    public static class CreateFamily
    {
        [FunctionName("CreateFamily")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "createfamily")] HttpRequest req,
            [DurableClient] IDurableEntityClient client)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<FamilyDto>(requestBody);

            var entityId = new EntityId(nameof(Family), data.FamilyName);

            var stateResponse = await client.ReadEntityStateAsync<Family>(entityId);

            if(stateResponse.EntityExists == false)
            {
                await client.SignalEntityAsync<IFamilyOperations>(entityId, proxy => proxy.New(data));

                return new OkObjectResult("Family created.");
            }
            else
            {
                return new BadRequestObjectResult("Family name already exists.");
            }
        }
    }
}
