using Newtonsoft.Json;

namespace SkilljarApi.Models
{
    public class ListResponse<T> where T : new()
    {
        [JsonProperty("count")]
        public long Count { get; set; }  = default;
        [JsonProperty("next")]
        public string Next { get; set; } = string.Empty;
        [JsonProperty("previous")]
        public string Previous { get; set; } = string.Empty;
        [JsonProperty("results")]
        public IEnumerable<T> Results { get; set; }

        public ListResponse()
        {
            Results = [];
        }
    }

}
