using Newtonsoft.Json;
using SkilljarApi.Models;

namespace SkilljarApi.Helpers
{
    internal static class ApiResponseHelper
    {
        internal static async Task<IEnumerable<T>> ProcessListResponse<T>(HttpClient client, string responseContent) where T : new()
        {
            List<T> returnList = new List<T>();

            var listResponse = JsonConvert.DeserializeObject<ListResponse<T>>(responseContent) ?? new ListResponse<T>();
            returnList.AddRange(listResponse.Results);

            while (listResponse.Next != null)
            {
                var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, listResponse.Next)).ConfigureAwait(false);
                responseContent = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

                listResponse = JsonConvert.DeserializeObject<ListResponse<T>>(responseContent) ?? new ListResponse<T>();
                returnList.AddRange(listResponse.Results);
            }

            return returnList;
        }
    }
}
