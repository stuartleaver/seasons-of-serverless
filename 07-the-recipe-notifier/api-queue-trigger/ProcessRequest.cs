using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Octokit;
using RecipeNotifier.Entities;
using SendGrid.Helpers.Mail;

namespace RecipeNotifier.Api
{
    public static class ProcessRequest
    {
        [FunctionName("ProcessRequest")]
        public static void Run([QueueTrigger("recipeconnectorqueue", Connection = "AzureWebJobsStorage")] RecipeRequest request,
            [SendGrid(ApiKey = "SendGridApiKey")] out SendGridMessage message,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {request.Email}");

            var jollofRiceRecipe = GetGist(Environment.GetEnvironmentVariable("GitHubGistId")).Result;

            message = new SendGridMessage();
            message.AddTo(string.IsNullOrEmpty(request.Email) ? "noreply@recipeconnector.cloud" : request.Email);
            message.AddContent("text/html", $"<html><body><p>Here is your jollof rice recipe from the following reginal office.</p><br /><p>Country Subdivision - {request.CountrySubdivision}</p><p>Municipality - {request.Municipality}</p><p>Country - {request.Country}</p><br /><p>{jollofRiceRecipe}</p></body></html>");
            message.SetFrom(new SendGrid.Helpers.Mail.EmailAddress("noreply@recipeconnector.cloud"));
            message.SetSubject("Recipe Connector - Your jollof rice recipe");
        }

        private static async Task<string> GetGist(string gistId)
        {
            var oktokit = new GitHubClient(new Octokit.ProductHeaderValue("TestGitHubAPI"));

            var gist = await oktokit.Gist.Get(gistId);

            return gist.Files[gist.Files.Keys.ToList()[0]].Content;
        }
    }
}
