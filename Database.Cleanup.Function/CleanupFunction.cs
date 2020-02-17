using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
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
            log.LogInformation($"Cleanup Function Started at: {DateTime.Now}");
            await HandleCleanup(log, context);
            log.LogInformation($"Cleanup Function Ended at: {DateTime.Now}");
        }

        private static async Task HandleCleanup(ILogger log, ExecutionContext context)
        {
            var jsonObj = File.ReadAllText(Path.Combine(context.FunctionAppDirectory, @"appsettings.json"));
            var appDetails = JsonConvert.DeserializeObject<AppDetil>(jsonObj);
            if (appDetails == null || appDetails.AppDetails.Length == 0)
            {
                log.LogError($"No apps found in config for cleanup");
                return;
            }

            foreach (var app in appDetails.AppDetails)
            {

                if (string.IsNullOrEmpty(app.AuthKey))
                {
                    log.LogError($"AuthKey is empty for App { app.AppName}");
                    return;
                }

                try
                {
                    var body = string.Empty;
                    using (var client = new HttpClient())
                    {
                        using (var request = new HttpRequestMessage(HttpMethod.Post, app.Url))
                        {
                            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            request.Headers.Add("AuthKey", app.AuthKey);
                            request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                            using (HttpResponseMessage response = await client.SendAsync(request))
                            {
                                if (response.IsSuccessStatusCode)
                                {
                                    log.LogInformation($"Cleanup successful. App Name:{app.AppName}, URL: {app.Url}");
                                }
                                else
                                {
                                    log.LogError($"Cleanup unsuccessful. App Name:{app.AppName}, URL: {app.Url} " +
                                        $"status code: {response.StatusCode} ," +
                                        $"Error Message: {response.RequestMessage}");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.LogError($"Exception ocured during Cleanup. App Name:{app.AppName}, URL: {app.Url} ," +
                        $"Exception message: {ex.Message}");
                }
            }
        }
    }
}
