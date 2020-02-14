using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Database.Cleanup.Function
{
    public static class CleanupFunction
    {
        [FunctionName("CleanupFunction")]
        public static async void Run([TimerTrigger("0 0 9 * * MON")]TimerInfo myTimer, ILogger log, ExecutionContext context) 
        {
            // (0 0 9 * * MON) == every mondaty 9 am  
            // Use of debug (0 */1 * * * *) == every minute
            log.LogInformation($"C# Timer trigger function Started at: {DateTime.Now}");
            await HandleCleanup(log, context);
            log.LogInformation($"C# Timer trigger function Ended at: {DateTime.Now}");
        }

        private static async Task HandleCleanup(ILogger log, ExecutionContext context)
        {
            var config = new ConfigurationBuilder()
                       .SetBasePath(context.FunctionAppDirectory)
                       .AddJsonFile("local.settings.json", optional: false, reloadOnChange: true)
                       .AddEnvironmentVariables()
                       .Build();
            var appDetails = config["AppDetails"];
            var authKey = config["AuthKey"];
            if (string.IsNullOrEmpty(appDetails))
            {
                log.LogError($"AppDetails is empty");
                return;
            }
            if (string.IsNullOrEmpty(authKey))
            {
                log.LogError($"AuthKey is empty");
                return;
            }

            var appUrls = appDetails.Split(';');
            foreach (var url in appUrls)
            {
                try
                {
                    var body = string.Empty;
                    using (var client = new HttpClient())
                    {
                        using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                        {
                            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            request.Headers.Add("AuthKey", authKey);
                            request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                            using (HttpResponseMessage response = await client.SendAsync(request))
                            {
                                if (response.IsSuccessStatusCode)
                                {
                                    log.LogInformation($"Cleanup successful, URL: {url}");
                                }
                                else
                                {
                                    log.LogError($"Cleanup unsuccessful, URL: {url} " +
                                        $"status code: {response.StatusCode} ," +
                                        $"Error Message: {response.RequestMessage}");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.LogError($"Exception ocured during Cleanup. URL: {url} ," +
                        $"Exception message: {ex.Message}");
                }
            }
        }
    }
}
