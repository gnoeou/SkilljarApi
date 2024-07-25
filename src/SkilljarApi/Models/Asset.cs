using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkilljarApi.Models
{
    public class Asset
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }
        [JsonProperty("embed_link_url")]
        public string? EmbedLinkUrl { get; set; }
        [JsonProperty("download_url")]
        public string? DownloadUrl { get; set; }
        [JsonProperty("aspect_ratio")]
        public string? AspectRatio { get; set; } = "16:9";
        [JsonProperty("sync_completion")]
        public bool SyncCompletion { get; set; } = false;
    }

}
