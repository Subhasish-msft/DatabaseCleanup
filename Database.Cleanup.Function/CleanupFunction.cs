using Microsoft.Azure.WebJobs;
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
        public static async void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function Started at: {DateTime.Now}");
            await HandleCleanup();
            log.LogInformation($"C# Timer trigger function Ended at: {DateTime.Now}");
        }

        private static async Task HandleCleanup()
        {
            try
            {
                var appsettingList = System.Environment.GetEnvironmentVariable("AppDetails");
                var accessToken = await GetAccessTokenAsync();
                var body = string.Empty;
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Post, appsettingList))
                    {
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                        request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                        using (HttpResponseMessage response = await client.SendAsync(request))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task<string> GetAccessTokenAsync()
        {
            return string.Empty;
        }
    }
}
