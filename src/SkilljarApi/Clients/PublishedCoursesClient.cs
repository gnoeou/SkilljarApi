using Newtonsoft.Json;
using SkilljarApi.Helpers;
using SkilljarApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SkilljarApi.Clients
{
    public class PublishedCoursesClient
    {
        HttpClient _httpClient;

        public PublishedCoursesClient(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<PublishedCourse> ReadAsync(string domainName, string publshedCourseId)
        { 
            var request = ApiRequestHelper.BuildReadRequest(ApiUrls.PublishedCourse(domainName, publshedCourseId));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<PublishedCourse>(content)!;
        }

        public async Task<IEnumerable<PublishedCourse>> ListAllAsync(string domainName)
        {
            var request = ApiRequestHelper.BuildListRequest(ApiUrls.PublishedCourses(domainName));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return await ApiResponseHelper.ProcessListResponse<PublishedCourse>(_httpClient, content).ConfigureAwait(false);
        }


        public async IAsyncEnumerable<ListResponse<PublishedCourse>> ListAsPages(string domainName)
        {
            var request = ApiRequestHelper.BuildListRequest(ApiUrls.PublishedCourses(domainName));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            var listResponse = JsonConvert.DeserializeObject<ListResponse<PublishedCourse>>(content) ?? new ListResponse<PublishedCourse>();

            yield return listResponse;
            while (listResponse.Next != null)
            {
                response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, listResponse.Next)).ConfigureAwait(false);
                content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

                listResponse = JsonConvert.DeserializeObject<ListResponse<PublishedCourse>>(content) ?? new ListResponse<PublishedCourse>();
                yield return listResponse;
            }
        }
    }
}
