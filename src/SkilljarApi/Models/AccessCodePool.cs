using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkilljarApi.Models
{

    public class AccessCodePool
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("active")]
        public bool Active { get; set; }
        [JsonProperty("start_date")]
        public DateTime? StartDate { get; set; }
        [JsonProperty("end_date")]
        public DateTime? EndDate { get; set; }
        [JsonProperty("expire_linked_to_domain_membership")]
        public bool WillExpire { get; set; }
        [JsonProperty("channel")]
        public string Channel { get; set; }
        [JsonProperty("code_cound")]
        public int CodeCount { get; set; }
    }

}
