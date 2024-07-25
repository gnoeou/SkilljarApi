using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SkilljarApi.Helpers
{
    internal static class ApiRequestHelper
    {
        internal static HttpRequestMessage BuildReadRequest(Uri requestUri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(SkilljarApiDefaults.ContentType));
            return request;
        }

        internal static HttpRequestMessage BuildListRequest(Uri requestUri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(SkilljarApiDefaults.ContentType));
            return request;
        }

        internal static HttpRequestMessage BuildPostRequest<T>(Uri requestUri, T newObject)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(SkilljarApiDefaults.ContentType));

            request.Content = new StringContent(JsonConvert.SerializeObject(newObject));
            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(SkilljarApiDefaults.ContentType);

            return request;
        }

        internal static HttpRequestMessage BuildPutRequest<T>(Uri requestUri, T updatedObject)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(SkilljarApiDefaults.ContentType));

            request.Content = new StringContent(JsonConvert.SerializeObject(updatedObject));
            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(SkilljarApiDefaults.ContentType);

            return request;
        }

        internal static HttpRequestMessage BuildPatchRequest<T>(Uri requestUri, T updatedObject)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, requestUri);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(SkilljarApiDefaults.ContentType));

            request.Content = new StringContent(JsonConvert.SerializeObject(updatedObject));
            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(SkilljarApiDefaults.ContentType);

            return request;
        }

        internal static HttpRequestMessage BuildDeleteRequest(Uri requestUri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(SkilljarApiDefaults.ContentType));

            return request;
        }
    }
}
