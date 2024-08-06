# SkilljarApi - Skilljar API Client Library for .NET
A quick attempt to create a API client for the [Skilljar LMS API](https://api.skilljar.com/docs/). For more information and how to get started with the Skilljar API, see [this help article](https://support.skilljar.com/hc/en-us/articles/203811260-Getting-started-with-the-Skilljar-API). 

**Note:** this library is incomplete. See table below for status of each object defined in the Skilljar API. 

|API Object	|Status|
|-----------|:------:|
|assets		|![Complete](https://img.icons8.com/?size=25&id=9fp9k4lPT8us&format=png&color=000000 "Completed Check Mark")|
|courses	|![Complete](https://img.icons8.com/?size=25&id=9fp9k4lPT8us&format=png&color=000000 "Completed Check Mark")|
|domains	|![In Process](https://img.icons8.com/?size=25&id=PUULuXvUfB6u&format=png&color=000000 "In Process Gear")		|
|group-categories	|		|
|groups	|		|
|ilt-instructors	|		|
|ilt-multi-session-events|		|
|ilt-sessions	|		|
|lesson-progress|		|
|lessons|		|
|offers	|		|
|paths	|		|
|ping	|![Complete](https://img.icons8.com/?size=25&id=9fp9k4lPT8us&format=png&color=000000 "Completed Check Mark")|
|progresstokens	|		|
|promo-code-pools|		|
|promo-codes|		|
|purchases	|		|
|question-banks	|		|
|quiz-questions	|		|
|quizzes	|		|
|training-credit-codes	|		|
|users	|		|
|vilt-session-events	|		|
|vilt-session-registrations	|		|
|web-packages	|		|
|webhooks	|		|

[Icons Provided by Icons8](https://icons8.com)

## Example Usage
To get started, the Skilljar API client requires an HttpClient object that has been created with the API Base Address and Authentication Header:
```csharp
var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey));

HttpClient httpClient = new()
{
    BaseAddress = new Uri("https://api.skilljar.com/v1/"),
    Timeout = new TimeSpan(0,0,30)
};

httpClient.DefaultRequestHeaders.Clear();
httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authString);

```
After creating your HttpClient instance, you can pass it to your SkilljarApiClient and begin working with the supported API calls:

### Domain

Get a list of domains for your skilljar instance
```csharp
var client = new SkilljarApiClient(httpClient);
var domainList = await client.Domains.ListAllAsync();
```
List all Published Courses for a particular domain
```csharp
var client = new SkilljarApiClient(httpClient);
var publishedCourses = await client.Domains.PublishedCourses.ListAllAsync("your-domain-name");
```

### Courses
Read details of a specific course
```csharp
var client = new SkilljarApiClient(httpClient);
var publishedCourses = await client.Courses.ReadAsync("your-course-id");
```