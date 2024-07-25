using Newtonsoft.Json;
using SkilljarApi.Helpers;
using SkilljarApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SkilljarApi.Clients
{
    /// <summary>
    /// A client for Skilljar's Assets API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://api.skilljar.com/docs/#assets"></a> for more details.
    /// </remarks>
    public class AssetsClient
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new Assets API client. 
        /// </summary>
        /// <param name="client"></param>
        public AssetsClient(HttpClient client)
        {
            _httpClient = client;
        }

        /// <summary>
        /// Read the specified asset by id.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#assets-read">API Documentation</a> for more information.
        /// </remarks>
        /// <param name="courseId">The Id of the asset</param>
        /// <exception cref="SkilljarApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="Asset"/> instance. </returns>
        public async Task<Asset> ReadAsync(string assetId)
        {
            var request = ApiRequestHelper.BuildReadRequest(ApiUrls.Asset(assetId));
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Asset>(content)!;
        }
        /// <summary>
        /// Gets all assets
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#assets-list">API Documentation</a> for more information.  
        /// The default page size is 25.
        /// </remarks>
        /// <returns>A <see cref="IEnumerable{Asset}"/>of <see cref="Asset"/>.</returns>

        public async Task<IEnumerable<Asset>> ListAllAsync()
        {
            var request = ApiRequestHelper.BuildListRequest(ApiUrls.Assets());
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return await ApiResponseHelper.ProcessListResponse<Asset>(_httpClient, content).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets all assets on a page by page basis
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#assets-list">API Documentation</a> for more information.  
        /// The default page size is 25.
        /// </remarks>
        /// <returns>A <see cref="IAsyncEnumerable{ListResponse}"/>of <see cref="ListResponse{Asset}"/>.</returns>
        public async IAsyncEnumerable<ListResponse<Asset>> ListAsPages(int pageSize = 25)
        {
            var request = ApiRequestHelper.BuildListRequest(ApiUrls.Assets(pageSize));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            var listResponse = JsonConvert.DeserializeObject<ListResponse<Asset>>(content) ?? new ListResponse<Asset>();

            yield return listResponse;

            while (listResponse.Next != null)
            {

                response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, listResponse.Next)).ConfigureAwait(false);
                content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

                listResponse = JsonConvert.DeserializeObject<ListResponse<Asset>>(content) ?? new ListResponse<Asset>();
                yield return listResponse;
            }
        }

        /// <summary>
        /// Creates a new asset
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#assets-create">API Documenation</a> for more information. 
        /// </remarks>
        /// <param name="newAsset"> A <see cref="Asset"/> instance describing the new course.</param>
        /// <returns>A <see cref="Asset"/> instance for the created course.</returns>
        public async Task<Asset> CreateCourse(Asset newAsset)
        {
            var request = ApiRequestHelper.BuildPostRequest<Asset>(ApiUrls.Asset(),newAsset);

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Asset>(content)!;
        }
    }
}
