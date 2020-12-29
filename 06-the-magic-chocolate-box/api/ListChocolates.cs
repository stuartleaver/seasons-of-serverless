using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MagicChocolateBox.Chocolates;
using System.Linq;

namespace MagicChocolateBox.Api
{
    public static class ListChocolates
    {
        [FunctionName("ListChocolates")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listchocolates/{chocolateBoxSize}")] HttpRequest req,
            int chocolateBoxSize,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (chocolateBoxSize < 1 || chocolateBoxSize > 3)
            {
                return new BadRequestObjectResult("Invalid chocolate box size. Valid sizes are 1 (Small), 2 (Medium) or 3 (Large)");
            }

            ChocolateBox chocolateBox;

            switch ((ChocolateBoxSize)chocolateBoxSize)
            {
                case ChocolateBoxSize.Small:
                default:
                    chocolateBox = new SmallChocolateBox();
                    chocolateBox.Create();

                    break;

                case ChocolateBoxSize.Medium:
                    chocolateBox = new MediumChocolateBox();
                    chocolateBox.Create();

                    break;
                case ChocolateBoxSize.Large:
                    chocolateBox = new LargeChocolateBox();
                    chocolateBox.Create();

                    break;
            }

            var chocolates = chocolateBox.Chocolates.OrderBy(x => x.Name).Select(x => x.Name).ToList();

            return new OkObjectResult(chocolates);
        }
    }
}
