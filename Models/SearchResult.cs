using Newtonsoft.Json;

namespace Models
{
    public class SearchResult<T> where T : class
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next")]
        public object Next { get; set; }

        [JsonProperty("last")]
        public object Last { get; set; }

        [JsonProperty("results")]
        public List<T> Results { get; set; }
    }
}