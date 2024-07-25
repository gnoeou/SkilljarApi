using Newtonsoft.Json;
using SkilljarApi.Helpers;
using SkilljarApi.Models;

namespace SkilljarApi.Clients
{
    /// <summary>
    /// A client for Skilljar's Courses API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://api.skilljar.com/docs/#courses"></a> for more details.
    /// </remarks>
    public class CoursesClient
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new Courses API client. 
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> instance configured with the base address and authorization headers.</param>
        public CoursesClient(HttpClient client)
        {
            _httpClient = client;
        }

        /// <summary>
        /// Read the specified course.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#courses-read">API Documentation</a> for more information.
        /// </remarks>
        /// <param name="courseId">The Id of the course</param>
        /// <exception cref="SkilljarApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="Course"/> instance. </returns>
        public async Task<Course> ReadAsync(string courseId)
        {
            var request = ApiRequestHelper.BuildReadRequest(ApiUrls.Course(courseId));
            
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Course>(content)!;
        }

        /// <summary>
        /// Gets all courses
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#courses-list">API Documentation</a> for more information.  
        /// The default page size is 25.
        /// </remarks>
        /// <returns>A <see cref="IEnumerable{Course}"/>of <see cref="Course"/>.</returns>
        public async Task<IEnumerable<Course>> ListAllAsync(int pageSize = 25)
        {
            var request = ApiRequestHelper.BuildListRequest(ApiUrls.Courses(pageSize));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return await ApiResponseHelper.ProcessListResponse<Course>(_httpClient, content).ConfigureAwait(false);  
        }

        /// <summary>
        /// Gets all courses on a page by page basis
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#courses-list">API Documentation</a> for more information.  
        /// The default page size is 25.
        /// </remarks>
        /// <returns>A <see cref="IAsyncEnumerable{ListResponse}"/>of <see cref="ListResponse{Course}"/>.</returns>
        public async IAsyncEnumerable<ListResponse<Course>> ListAsPages(int pageSize = 25)
        {
            var request = ApiRequestHelper.BuildListRequest(ApiUrls.Courses(pageSize));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            var listResponse = JsonConvert.DeserializeObject<ListResponse<Course>>(content) ?? new ListResponse<Course>();

            yield return listResponse;

            while (listResponse.Next != null)
            {

                response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, listResponse.Next)).ConfigureAwait(false);
                content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

                listResponse = JsonConvert.DeserializeObject<ListResponse<Course>>(content) ?? new ListResponse<Course>();
                yield return listResponse;
            }
        }

        /// <summary>
        /// Creates a new course
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#courses-create">API Documenation</a> for more information. 
        /// </remarks>
        /// <param name="newCourse"> A <see cref="Course"/> instance describing the new course.</param>
        /// <returns>A <see cref="Course"/> instance for the created course.</returns>
        public async Task<Course> CreateAsync(Course newCourse)
        {
            var request = ApiRequestHelper.BuildPostRequest<Course>(ApiUrls.Course(), newCourse);
            
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Course>(content)!;
        }

        /// <summary>
        /// Updates all fields for a particular course.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#courses-update">API Documenation</a> for more information. 
        /// </remarks>
        /// <param name="updatedCourse"> A <see cref="Course"/> instance describing the updated course.</param>
        /// <returns>A <see cref="Course"/> instance for the updated course.</returns>
        public async Task<Course> UpdateAsync(string courseId, Course updatedCourse)
        {
            var request = ApiRequestHelper.BuildPutRequest<Course>(ApiUrls.Course(courseId), updatedCourse);

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Course>(content)!;
        }

        /// <summary>
        /// Updates all fields for a particular course.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://api.skilljar.com/docs/#courses-partial_update">API Documenation</a> for more information. 
        /// </remarks>
        /// <param name="updatedCourse"> A <see cref="Course"/> instance describing the updated course.</param>
        /// <returns>A <see cref="Course"/> instance for the updated course.</returns>
        public async Task<Course> PartialUpdateAsync(string courseId, Course updatedCourse)
        {
            var request = ApiRequestHelper.BuildPatchRequest<Course>(ApiUrls.Course(courseId), updatedCourse);

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var content = await response.ProcessSkilljarApiResponse().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Course>(content)!;
        }
    }
}
