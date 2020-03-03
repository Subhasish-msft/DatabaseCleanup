namespace Database.Cleanup.Function.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// ConnectorCard class
    /// </summary>
    public class ConnectorCard
    {
        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("sections")]
        public List<Sections> Sections { get; set; }
    }

    public class Sections
    {
        [JsonProperty("facts")]
        public List<Fact> Facts { get; set; }

    }

    public class Fact
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
