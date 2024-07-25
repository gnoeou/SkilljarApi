using Newtonsoft.Json;
using SkilljarApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkilljarApi.Helpers
{
    internal static class HttpResponseMessageExtensions
    {
        public static async Task<string> ProcessSkilljarApiResponse(this HttpResponseMessage response)
        {

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(content) ?? new ApiErrorResponse("Unknown Error Occured");
                throw new SkilljarApiException(errorResponse.Detail!);
            }

            return content;

        }
    }
}
