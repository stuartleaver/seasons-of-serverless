using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using MagicChocolateBox.Chocolates;

namespace MagicChocolateBox.Api
{
    public static class ListChocolateBoxes
    {
        [FunctionName("ListChocolateBoxes")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listchocolateboxes")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var chocolateBoxes = new List<ChocolateBox>();

            var smallChocolateBox = new SmallChocolateBox();
            smallChocolateBox.Create();
            chocolateBoxes.Add(smallChocolateBox);

            var mediumChocolateBox = new MediumChocolateBox();
            mediumChocolateBox.Create();
            chocolateBoxes.Add(mediumChocolateBox);

            var largeChocolateBox = new LargeChocolateBox();
            largeChocolateBox.Create();
            chocolateBoxes.Add(largeChocolateBox);

            return new OkObjectResult(chocolateBoxes);
        }
    }

}