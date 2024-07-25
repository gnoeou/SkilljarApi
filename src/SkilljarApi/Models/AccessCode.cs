using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkilljarApi.Models
{
    public class AccessCode
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("active")]
        public bool Active { get; set; }
        [JsonProperty("max_uses")]
        public int MaxUses { get; set; }
        [JsonProperty("use_count")]
        public int UseCount { get; set; }
        [JsonProperty("duration")]
        public int? Duration { get; set; }
        [JsonProperty("duration_unit")]
        public string DurationUnit { get; set; }
    }
}
