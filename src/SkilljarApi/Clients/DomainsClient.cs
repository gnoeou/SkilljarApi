using Newtonsoft.Json;
using SkilljarApi.Helpers;
using SkilljarApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SkilljarApi.Clients
{
    /// <summary>
    /// A client for Skilljar's Domains API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://api.skilljar.com/docs/#domains"></a> for more details.
    /// </remarks>
    public class DomainsClient
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new Domains API client. 
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> instance configured with the base address and authorization headers.</param>
        public DomainsClient(HttpClient client)
        {
            _httpClient = client;

            AccessCodePools = new AccessCodePoolsClient(client);
            PublishedCourses = new PublishedCoursesClient(client); 
            PublishedPaths = new PublishedPathesClient(client);

        }

        public AccessCodePoolsClient AccessCodePools; 
        public PublishedCoursesClient PublishedCourses; 
        public PublishedPathesClient PublishedPaths;

        /// <summary>
        /// Read the specified domain.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#domains-read">API Documentation</a> for more information.
        /// </remarks>
        /// <param name="courseId">The Id of the course</param>
        /// <exception cref="SkilljarApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="Domain"/> instance. </returns>
        public async Task<Domain> ReadAsync(string domainName) 
        {
            var request = ApiRequestHelper.BuildReadRequest(ApiUrls.Domain(domainName));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);
                
            return JsonConvert.DeserializeObject<Domain>(content)!;
        }

        /// <summary>
        /// Gets all domains
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#domains-list">API Documentation</a> for more information.  
        /// The default page size is 25.
        /// </remarks>
        /// <returns>A <see cref="IEnumerable{Domain}"/>of <see cref="Domain"/>.</returns>
        public async Task<IEnumerable<Domain>> ListAllAsync()
        {
            var request = ApiRequestHelper.BuildListRequest(ApiUrls.Domains());

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return await ApiResponseHelper.ProcessListResponse<Domain>(_httpClient, content).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets all domains on a page by page basis
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#domains-list">API Documentation</a> for more information.  
        /// The default page size is 25.
        /// </remarks>
        /// <returns>A <see cref="IAsyncEnumerable{ListResponse}"/>of <see cref="ListResponse{Domain}"/>.</returns>
        public async IAsyncEnumerable<ListResponse<Domain>> ListAsPages()
        {
            var request = ApiRequestHelper.BuildListRequest(ApiUrls.Domains());

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            var listResponse = JsonConvert.DeserializeObject<ListResponse<Domain>>(content) ?? new ListResponse<Domain>();

            yield return listResponse;

            while (listResponse.Next != null)
            {

                response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, listResponse.Next)).ConfigureAwait(false);
                content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

                listResponse = JsonConvert.DeserializeObject<ListResponse<Domain>>(content) ?? new ListResponse<Domain>();
                yield return listResponse;
            }
        }

        /// <summary>
        /// Updates all fields for a particular domain.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#domains-partial_update">API Documenation</a> for more information. 
        /// </remarks>
        /// <param name="updatedDomain"> A <see cref="Domain"/> instance describing the updated course.</param>
        /// <returns>A <see cref="Domain"/> instance for the updated course.</returns>
        public async Task<Domain> UpdateAsync(string domainName, Domain updatedDomain)
        {
            var request = ApiRequestHelper.BuildPutRequest<Domain>(ApiUrls.Domain(domainName), updatedDomain);

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Domain>(content)!;
        }

        /// <summary>
        /// Updates all fields for a particular domain.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#domains-partial_update">API Documenation</a> for more information. 
        /// </remarks>
        /// <param name="updatedDomain"> A <see cref="Domain"/> instance describing the updated course.</param>
        /// <returns>A <see cref="Domain"/> instance for the updated course.</returns>
        public async Task<Domain> PartialUpdateAsync(string domainName, Domain updatedDomain)
        {
            var request = ApiRequestHelper.BuildPatchRequest<Domain>(ApiUrls.Domain(domainName), updatedDomain);

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Domain>(content)!;
        }
    }
}
