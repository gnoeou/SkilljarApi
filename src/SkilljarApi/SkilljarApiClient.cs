using SkilljarApi.Clients;
using SkilljarApi.Helpers;

namespace SkilljarApi
{

    public class SkilljarApiClient : ISkilljarClient
    {
        private readonly HttpClient restClient;
      
        public SkilljarApiClient(HttpClient httpClient)
        {
            restClient = httpClient;
            Courses = new CoursesClient(restClient);
            Domains = new DomainsClient(restClient);
            Assets = new AssetsClient(restClient);
        }

        public CoursesClient Courses { get; }
        public DomainsClient Domains { get; }
        public AssetsClient Assets { get; }

        /// <summary>
        /// Ping the API Endpoint.
        /// </summary>
        /// <exception cref="SkilljarApiException">Throws an exception if not authorized</exception>
        public async Task Ping()
        {
            var response = await restClient.GetAsync(ApiUrls.Ping()).ConfigureAwait(false);
            await response.ProcessSkilljarApiResponse().ConfigureAwait(false);
        }
    }

    public interface ISkilljarClient
    {
        Task Ping();

        public CoursesClient Courses { get; }
        public DomainsClient Domains{ get; }
        public AssetsClient Assets { get; }
    }
}
