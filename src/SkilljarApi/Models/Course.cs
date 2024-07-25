using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkilljarApi.Models
{
    public class Course
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
        [JsonProperty("enforce_sequential_navigation")]
        public bool EnforceSequentialNavigation { get; set; } = false;
        [JsonProperty("long_description_html")]
        public string LongDescriptionHtml { get; set; } = string.Empty;
        [JsonProperty("short_description")]
        public string ShortDescription { get; set; } = string.Empty;
        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;
        [JsonProperty("labels")]
        public string[] Labels { get; set; } = [];
        public Course()
        {
                
        }
    }
}
