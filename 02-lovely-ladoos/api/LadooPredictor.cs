using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;

namespace LadooPredictor.Api
{
    public static class LadooPredictor
    {
        [FunctionName("LadooPredictor")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "ladoopredictor")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string imageUrl = req.Query["imageUrl"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            imageUrl = imageUrl ?? data?.imageUrl;

            if (!IsValidImageExtension(imageUrl))
            {
                return new BadRequestObjectResult("Please supply an image URL in the imageUrl query parameter. Allowed file formats are either jpg, jpeg, png or bmp");
            }

            var endpoint = Environment.GetEnvironmentVariable("CUSTOM_VISION_ENDPOINT");
            var predictionKey = Environment.GetEnvironmentVariable("CUSTOM_VISION_PREDICTION_KEY");
            var projectId = Guid.Parse(Environment.GetEnvironmentVariable("CUSTOM_VISION_PROJECT_ID"));
            var publishedName = Environment.GetEnvironmentVariable("CUSTOM_VISION_PUBLISHED_NAME");

            try
            {
                var predictionApi = AuthenticatePrediction(endpoint, predictionKey);

                var prediction = await predictionApi.ClassifyImageUrlAsync(projectId, publishedName, new ImageUrl(imageUrl), null);

                var result = JsonConvert.SerializeObject(prediction.Predictions);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"An error occured - {ex.Message}");
            }
        }

        private static CustomVisionPredictionClient AuthenticatePrediction(string endpoint, string predictionKey)
        {
            // Create a prediction endpoint, passing in the obtained prediction key
            CustomVisionPredictionClient predictionApi = new CustomVisionPredictionClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.ApiKeyServiceClientCredentials(predictionKey))
            {
                Endpoint = endpoint
            };
            return predictionApi;
        }

        private static bool IsValidImageExtension(string imageUrl)
        {
            return !string.IsNullOrEmpty(imageUrl) && (
                imageUrl.EndsWith(".jpg")
                || imageUrl.EndsWith("jpeg")
                || imageUrl.EndsWith("png")
                || imageUrl.EndsWith("gif")
            );
        }
    }
}