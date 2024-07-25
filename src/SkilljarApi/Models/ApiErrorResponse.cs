using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkilljarApi.Models
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse() { }
        public ApiErrorResponse(string message) { }
        [JsonProperty("detail")]
        public string? Detail { get; set; } = string.Empty;
    }
}
