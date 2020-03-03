namespace Database.Cleanup.Function.Models
{
    public class AppDetails
    {
        public string AppName { get; set; }
        public string Url { get; set; }
        public string AuthKey { get; set; }
    }
    public class AppDetail
    {
        public string ChannelWebhookUrl { get; set; }
        public AppDetails[] AppDetails { get; set; }
    }
}
