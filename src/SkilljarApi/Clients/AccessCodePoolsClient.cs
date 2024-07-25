using Newtonsoft.Json;
using SkilljarApi.Helpers;
using SkilljarApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SkilljarApi.Clients
{
    /// <summary>
    /// A client for Skilljar's Access Pools API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://api.skilljar.com/docs/#domain-access-code-pools-list"></a> for more details.
    /// </remarks>
    public class AccessCodePoolsClient
    {
        HttpClient _httpClient;

        /// <summary>
        /// Initializes a new Access Code Pool API client. 
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> instance configured with the base address and authorization headers.</param>
        public AccessCodePoolsClient(HttpClient client)
        {
            _httpClient = client;
            AccessCodes = new AccessCodesClient(client);
        }

        public AccessCodesClient AccessCodes;

        /// <summary>
        /// Read the specified Access Code Pool.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#domains-access-code-pools-read">API Documentation</a> for more information.
        /// </remarks>
        /// <param name="accessCodePoolId">The Id of the course</param>
        /// <exception cref="SkilljarApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="AccessCodePool"/> instance. </returns>
        public async Task<AccessCodePool> ReadAsync(string domainName, string accessCodePoolId)
        { 
            var request = ApiRequestHelper.BuildReadRequest(ApiUrls.AccessCodePool(domainName, accessCodePoolId));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<AccessCodePool>(content)!;
        }

        /// <summary>
        /// Gets all Access Code Pools
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#domains-access-code-pools-list">API Documentation</a> for more information.  
        /// </remarks>
        /// <returns>A <see cref="IEnumerable{AccessCodePool}"/>of <see cref="AccessCodePool"/>.</returns>
        public async Task<IEnumerable<AccessCodePool>> ListAllAsync(string domainName)
        {
            var request = ApiRequestHelper.BuildListRequest(ApiUrls.AccessCodePools(domainName));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return await ApiResponseHelper.ProcessListResponse<AccessCodePool>(_httpClient, content).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets all access code pools on a page by page basis
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#domains-access-code-pools-list">API Documentation</a> for more information.  
        /// </remarks>
        /// <returns>A <see cref="IAsyncEnumerable{ListResponse}"/>of <see cref="ListResponse{AccessCodePool}"/>.</returns>
        public async IAsyncEnumerable<ListResponse<AccessCodePool>> ListAsPages(string domainName)
        {
            var request = ApiRequestHelper.BuildListRequest(ApiUrls.AccessCodePools(domainName));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            var listResponse = JsonConvert.DeserializeObject<ListResponse<AccessCodePool>>(content) ?? new ListResponse<AccessCodePool>();

            yield return listResponse;

            while (listResponse.Next != null)
            {

                response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, listResponse.Next)).ConfigureAwait(false);
                content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

                listResponse = JsonConvert.DeserializeObject<ListResponse<AccessCodePool>>(content) ?? new ListResponse<AccessCodePool>();
                yield return listResponse;
            }
        }

        /// <summary>
        /// Creates a new Access Code Pool
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#domains-access-code-pools-create">API Documenation</a> for more information. 
        /// </remarks>
        /// <param name="newAccessCodePool"> A <see cref="AccessCodePool"/> instance describing the new course.</param>
        /// <returns>A <see cref="AccessCodePool"/> instance for the created access code pool.</returns>
        public async Task<AccessCodePool> CreateAsync(string domainName, AccessCodePool newAccessCodePool)
        {
            var request = ApiRequestHelper.BuildPostRequest<AccessCodePool>(ApiUrls.AccessCodePools(domainName), newAccessCodePool);

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<AccessCodePool>(content)!;
        }

        /// <summary>
        /// Updates all fields for a particular access code pool.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#domains-access-code-pools-update">API Documenation</a> for more information. 
        /// </remarks>
        /// <param name="updatedAccessCodePool"> A <see cref="AccessCodePool"/> instance describing the updated course.</param>
        /// <returns>A <see cref="AccessCodePool"/> instance for the updated course.</returns>
        public async Task<AccessCodePool> UpdateAsync(string domainName,string accessCodePoolId, AccessCodePool updatedAccessCodePool)
        {
            var request = ApiRequestHelper.BuildPutRequest<AccessCodePool>(ApiUrls.AccessCodePool(domainName,accessCodePoolId), updatedAccessCodePool);

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<AccessCodePool>(content)!;
        }

        /// <summary>
        /// Updates all fields for a particular access code pool.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#domains-access-code-pools-update">API Documenation</a> for more information. 
        /// </remarks>
        /// <param name="updatedAccessCodePool"> A <see cref="AccessCodePool"/> instance describing the updated course.</param>
        /// <returns>A <see cref="AccessCodePool"/> instance for the updated course.</returns>
        public async Task<AccessCodePool> PartialUpdateAsync(string domainName, string accessCodePoolId, AccessCodePool updatedAccessCodePool)
        {
            var request = ApiRequestHelper.BuildPutRequest<AccessCodePool>(ApiUrls.AccessCodePool(domainName, accessCodePoolId), updatedAccessCodePool);

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<AccessCodePool>(content)!;
        }

        /// <summary>
        /// Deletes the specified Access Code Pool
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="accessPoolId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string domainName, string accessPoolId)
        { 
            var request = ApiRequestHelper.BuildDeleteRequest(ApiUrls.AccessCodePool(domainName,accessPoolId));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            if (string.IsNullOrEmpty(content))
                return true;
                    
            return false;
        }
    }
}
