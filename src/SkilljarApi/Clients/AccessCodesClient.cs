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
    public class AccessCodesClient
    {
        private HttpClient _httpClient;
        
        public AccessCodesClient(HttpClient client) 
        {
            _httpClient = client;
        }

        /// <summary>
        /// Read the specified Access Code .
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#domains-access-code-pools-access-codes-read">API Documentation</a> for more information.
        /// </remarks>
        /// <param name="accessCodeId">The Id of the Access Code</param>
        /// <exception cref="SkilljarApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="AccessCode"/> instance. </returns>
        public async Task<AccessCode> ReadAsync(string domainName, string accessCodePoolId, string accessCodeId)
        {
            var request = ApiRequestHelper.BuildReadRequest(ApiUrls.AccessCode(domainName, accessCodePoolId, accessCodeId));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<AccessCode>(content)!;
        }

        /// <summary>
        /// Gets all Access Codes for a specified access code pool
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#domains-access-code-pools-access-codes-list">API Documentation</a> for more information.  
        /// </remarks>
        /// <param name="domainName">The Domain Name</param>
        /// <param name="accessCodePoolId">the Access Code Pool Id</param>
        /// <returns>A <see cref="IEnumerable{AccessCode}"/>of <see cref="AccessCode"/>.</returns>
        public async Task<IEnumerable<AccessCode>> ListAllAsync(string domainName,string accessCodePoolId)
        {
            var request = ApiRequestHelper.BuildListRequest(ApiUrls.AccessCodes(domainName,accessCodePoolId));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return await ApiResponseHelper.ProcessListResponse<AccessCode>(_httpClient, content).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets all access codes for a specified access code pool on a page by page basis
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#domains-access-code-pools-access-codes-list">API Documentation</a> for more information.  
        /// </remarks>
        /// <returns>A <see cref="IAsyncEnumerable{ListResponse}"/>of <see cref="ListResponse{AccessCode}"/>.</returns>
        public async IAsyncEnumerable<ListResponse<AccessCode>> ListAsPages(string domainName, string accessCodePoolId)
        {
            var request = ApiRequestHelper.BuildListRequest(ApiUrls.AccessCodes(domainName, accessCodePoolId));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            var listResponse = JsonConvert.DeserializeObject<ListResponse<AccessCode>>(content) ?? new ListResponse<AccessCode>();

            yield return listResponse;

            while (listResponse.Next != null)
            {

                response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, listResponse.Next)).ConfigureAwait(false);
                content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

                listResponse = JsonConvert.DeserializeObject<ListResponse<AccessCode>>(content) ?? new ListResponse<AccessCode>();
                yield return listResponse;
            }
        }

        /// <summary>
        /// Creates a new Access Code Pool
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#domains-access-code-pools-access-codes-create">API Documenation</a> for more information. 
        /// </remarks>
        /// <param name="newAccessCode"> A <see cref="AccessCodePool"/> instance describing the new course.</param>
        /// <returns>A <see cref="AccessCodePool"/> instance for the created access code pool.</returns>
        public async Task<AccessCode> CreateAsync(string domainName, string accessCodePoolId, AccessCode newAccessCode)
        {
            var request = ApiRequestHelper.BuildPostRequest<AccessCode>(ApiUrls.AccessCodes(domainName, accessCodePoolId), newAccessCode);

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<AccessCode>(content)!;
        }

        /// <summary>
        /// Updates all fields for a particular access code.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#domains-access-code-pools-access-codes-update">API Documenation</a> for more information. 
        /// </remarks>
        /// <param name="updatedAccessCode"> A <see cref="AccessCode"/> instance describing the updated course.</param>
        /// <returns>A <see cref="AccessCode"/> instance for the updated course.</returns>
        public async Task<AccessCode> UpdateAsync(string domainName, string accessCodePoolId, string accessCodeId, AccessCode updatedAccessCode)
        {
            var request = ApiRequestHelper.BuildPutRequest<AccessCode>(ApiUrls.AccessCode(domainName, accessCodePoolId, accessCodeId), updatedAccessCode);

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<AccessCode>(content)!;
        }

        /// <summary>
        /// Updates all fields for a particular access code.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#domains-access-code-pools-access-codes-partial-update">API Documenation</a> for more information. 
        /// </remarks>
        /// <param name="updatedAccessCode"> A <see cref="AccessCode"/> instance describing the updated course.</param>
        /// <returns>A <see cref="AccessCode"/> instance for the updated course.</returns>
        public async Task<AccessCode> PartialUpdateAsync(string domainName, string accessCodePoolId, string accessCodeId, AccessCode updatedAccessCode)
        {
            var request = ApiRequestHelper.BuildPatchRequest<AccessCode>(ApiUrls.AccessCode(domainName, accessCodePoolId, accessCodeId), updatedAccessCode);

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<AccessCode>(content)!;
        }

        /// <summary>
        /// Deletes the specified Access Code Pool
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="accessPoolId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string domainName, string accessPoolId, string accessCodeId)
        {
            var request = ApiRequestHelper.BuildDeleteRequest(ApiUrls.AccessCode(domainName, accessPoolId, accessCodeId));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            if (string.IsNullOrEmpty(content))
                return true;

            return false;
        }
    }
}
