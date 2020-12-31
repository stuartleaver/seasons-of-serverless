using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using MagicChocolateBox.Api.Entities;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MagicChocolateBox.Api
{
    public static class Reserve
    {
        public static Family family;

        [FunctionName("Reserve")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "reserve")] HttpRequest req,
            [DurableClient] IDurableEntityClient client)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<ReservationDto>(requestBody);

            var entityId = new EntityId(nameof(Family), data.FamilyName);

            var stateResponse = await client.ReadEntityStateAsync<Family>(entityId);

            family = stateResponse.EntityState;

            if (stateResponse.EntityExists)
            {
                if (ExceededReservations(data.Name))
                {
                    return new BadRequestObjectResult("You've been caught with your hand on the chocolate box multiple times! You've reached your three reservations.");
                }

                if (ChocolateAlreadyReserved(data.Name, data.ChocolateName))
                {
                    return new BadRequestObjectResult($"You really like the {data.ChocolateName} chocolates. You can't reserve them more than once.");
                }

                var quantityRemaining = QuantityRemaining(data.ChocolateName);

                if (quantityRemaining == 0)
                {
                    return new BadRequestObjectResult($"Sorry, all of the {data.ChocolateName} chocolates are reserved.");
                }

                if (data.Quantity >= quantityRemaining)
                {
                    data.Quantity = quantityRemaining;

                    await client.SignalEntityAsync<IFamilyOperations>(entityId, proxy => proxy.AddReservation(data));

                    await client.SignalEntityAsync<IFamilyOperations>(entityId, proxy => proxy.UpdateRemainingChocolateQuantity(data));

                    return new OkObjectResult($"All of the {data.ChocolateName} chocolates where almost gone. You've reserved {data.Quantity}.");
                }

                await client.SignalEntityAsync<IFamilyOperations>(entityId, proxy => proxy.AddReservation(data));

                await client.SignalEntityAsync<IFamilyOperations>(entityId, proxy => proxy.UpdateRemainingChocolateQuantity(data));

                return new OkObjectResult($"Your reservation has been made.");
            }
            else
            {
                return new BadRequestObjectResult("Could not find your family name. Please 'create' your family to use this functionality");
            }
        }

        private static bool ExceededReservations(string name)
        {
            return family.Reservations.Count(x => x.Name == name) >= 3;
        }

        private static bool ChocolateAlreadyReserved(string name, string chocolateName)
        {
            return family.Reservations.Any(x => x.Name == name && x.ChocolateName == chocolateName);
        }

        private static int QuantityRemaining(string chocolateName)
        {
            return family.ChocolateBox.Chocolates.Where(x => x.Name == chocolateName).FirstOrDefault().Quantity;
        }

        private static bool ReservationsExceeded(string name, string chocolateName)
        {
            return family.Reservations.Any(x => x.Name == name && x.ChocolateName == chocolateName);
        }
    }
}
