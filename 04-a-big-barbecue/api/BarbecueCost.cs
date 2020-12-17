using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using BarbecueCost.Api.Entities;

namespace BarbecueCost.Api
{
    public static class BarbecueCost
    {
        private static List<BarbecueItem> barbecueItems;

        [FunctionName("BarbecueCost")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "barbecuecost")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<BarbecueDetails>(requestBody);
            barbecueItems = data.BarbecueItems;

            var barbequeItemsTotalCost = GetBarbequeItemsTotalCost(data.BarbecueItems);

            var costPerPerson = barbequeItemsTotalCost / data.Guests;

            var remainingBudget = data.Budget - barbequeItemsTotalCost;

            var response = new BarbequeCostResult
            {
                Name = data.Name,
                BarbequeItemsTotalCost = barbequeItemsTotalCost,
                CostPerPerson = costPerPerson,
                RemainingBudget = remainingBudget,
                BarbecueItems = barbecueItems
            };

            return new OkObjectResult(JsonConvert.SerializeObject(response));
        }

        private static double? GetBarbequeItemsTotalCost(List<BarbecueItem> items)
        {
            double? total = 0.00;

            barbecueItems = new List<BarbecueItem>();

            foreach (var item in items)
            {
                if (item.Cost >= 0 && item.Quantity > 0)
                {
                    barbecueItems.Add(item);

                    total += item.Cost * item.Quantity;
                }
            }

            return total;
        }
    }
}
