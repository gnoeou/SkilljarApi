using Newtonsoft.Json;
using SkilljarApi.Helpers;
using SkilljarApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkilljarApi.Clients
{
    public class PublishedPathesClient
    {
        HttpClient _httpClient;

        public PublishedPathesClient(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<PublishedPath> ReadAsync(string domainName, string publishedPathId)
        {
            var request = ApiRequestHelper.BuildReadRequest(ApiUrls.PublishedPath(domainName, publishedPathId));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<PublishedPath>(content)!;
        }

        public async Task<IEnumerable<PublishedPath>> ListAllAsync(string domainName)
        {
            var request = ApiRequestHelper.BuildListRequest(ApiUrls.PublishedPaths(domainName));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return await ApiResponseHelper.ProcessListResponse<PublishedPath>(_httpClient, content).ConfigureAwait(false);
        }


        public async IAsyncEnumerable<ListResponse<PublishedPath>> ListAsPages(string domainName)
        {
            var request = ApiRequestHelper.BuildListRequest(ApiUrls.PublishedPaths(domainName));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            var listResponse = JsonConvert.DeserializeObject<ListResponse<PublishedPath>>(content) ?? new ListResponse<PublishedPath>();

            yield return listResponse;
            while (listResponse.Next != null)
            {
                response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, listResponse.Next)).ConfigureAwait(false);
                content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

                listResponse = JsonConvert.DeserializeObject<ListResponse<PublishedPath>>(content) ?? new ListResponse<PublishedPath>();
                yield return listResponse;
            }
        }

    }
}
