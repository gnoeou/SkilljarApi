// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SkilljarApi;
using System.Text;

internal class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

        if (!config.Providers.First().TryGet("SkilljarApiKey", out var apiKey))
        {
            throw new Exception("Unable to locate SkilljarApiKey configuration value");
        }
                 
        var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey ?? throw new Exception("Api Key Is Required")));

        var builder = new HostBuilder()
            .ConfigureServices(services =>
                {
                    services
                    .AddHttpClient<ISkilljarClient, SkilljarApiClient>()
                    .ConfigureHttpClient(httpClient =>
                    {
                        httpClient.BaseAddress = new Uri("https://api.skilljar.com/v1/");
                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authString);
                    });
                });

        var host = builder.Build();

        try
        {
            var skilljar = host.Services.GetService<ISkilljarClient>();

            var listCourse = await skilljar.Courses.ListAllAsync();
            foreach (var course in listCourse)
            {
                Console.WriteLine($"Found '{course.Title}' in Skilljar Course Library");
            }

            var listAssets = await skilljar.Assets.ListAllAsync();
            foreach (var asset in listAssets)
            {
                Console.WriteLine($"Found '{asset.Name}' in Skilljar Asset Library");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}