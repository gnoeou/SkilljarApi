using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using SkilljarApi.Models;
using System.Text;

namespace SkilljarApi.Tests
{
    public class Tests
    {
        private SkilljarApiClient _client;

        [SetUp]
        public void Setup()
        {

            //Unit Tests rely on local secret storage for your Skilljar API key.  
            //Execute "dotnet user-secrets init" in the directory of your unit test project
            //Execute "dotnet user-secrets set SkilljarApiKey <<your skilljar api key>>" to create an entry for you Api Key
            //See below for more information:
            //https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=windows#enable-secret-storage
            
            
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Tests>()
                .Build();

            if (!config.Providers.First().TryGet("SkilljarApiKey", out var apiKey))
            {
                throw new Exception("Unable to locate SkilljarApiKey configuration value");
            }

            var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey ?? throw new Exception("Api Key Is Required")));

            HttpClient client = new()
            {
                BaseAddress = new Uri("https://api.skilljar.com/v1/"),
                Timeout = new TimeSpan(0,0,30)
            };
            
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authString);

            _client = new SkilljarApiClient(client);
        }

        //TODO: Expand Unit Test for Full Converage
        //TODO: Investigate Mocking and remove dependency on live API calls


        [Test]
        public async Task ReadCourseNotFound()
        {
            try
            {
                var response = await _client.Courses.ReadAsync("DoesNotExist");
            }
            catch (SkilljarApiException ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Not found."));
                Assert.That(ex, Is.TypeOf(typeof(SkilljarApiException)));
            }
        }

        [Test]
        public async Task ReadCourse()
        {
            var response = await _client.Courses.ReadAsync("24e9465l8oh95");
            Assert.That(response.Id, Is.EqualTo("24e9465l8oh95"));
        }

        [Test]
        public async Task ListAllCourses()
        {
            var response = await _client.Courses.ListAllAsync();
            Assert.That(response.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task PageAllCourses()
        {
            int pageCount = 0;
            await foreach (var page in _client.Courses.ListAsPages(50)) 
            {
                TestContext.WriteLine(page.Next);
                pageCount++;    
            }
            Assert.That(pageCount, Is.GreaterThan(0));  
        }

        [Test]
        public async Task PingApi()
        {
            await _client.Ping();
        }

        [Test]
        public async Task ReadAsset()
        {
            var response = await _client.Assets.ReadAsync("2k407myjbiw47");
            Assert.That(response.Id, Is.EqualTo("2k407myjbiw47"));
        }
    }
}