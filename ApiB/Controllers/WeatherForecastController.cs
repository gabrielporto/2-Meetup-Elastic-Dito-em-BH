using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>?> GetAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://api-c/WeatherForecast");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(result);
        }
    }
}
