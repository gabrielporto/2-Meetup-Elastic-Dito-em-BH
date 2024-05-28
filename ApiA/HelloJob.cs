using Quartz;
using System.Text.Json;

namespace ApiA
{
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello, Quartz.NET!");
            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://api-b/WeatherForecast");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();

            //return Task.CompletedTask;
        }
    }
}