using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkilljarApi.Models
{

    public class Domain
    {
        [JsonProperty("id")]    
        public string? Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("catalog_title")]
        public string? CatalogTitle { get; set; }
        [JsonProperty("private")]
        public bool IsPrivate { get; set; }
        [JsonProperty("access")]
        public DomainAccess Access { get; set; }
        [JsonProperty("access_code_message_html")]
        public string? AccessCodeMessageHtml{ get; set; }
        [JsonProperty("marketing_message")]
        public string? MarketingMessage { get; set; }

        public Domain()
        {
                
        }
    }


    public enum DomainAccess 
    {
        PUBLIC,
        PRIVATE_CODE,
        PRIVATE
    }

}
