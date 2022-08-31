using LifecycleTest.Code;
using Microsoft.AspNetCore.Mvc;

namespace LifecycleTest.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger, SingletonTest singleton, ScopeTest scope, TransientTest transient)
        {
            //SingletonTest test = new SingletonTest(new());
            //test.Show();

            Console.WriteLine("-----------");
            singleton.Show();
            Console.WriteLine($"request {singleton.Guid}");
            Console.WriteLine($"request {scope.Guid}");
            Console.WriteLine($"request {transient.Guid}");

            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}