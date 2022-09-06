using ActionWebApiTest.Code;
using Microsoft.AspNetCore.Mvc;

namespace ActionWebApiTest.Controllers
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
        private readonly TestService _testService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, TestService testService)
        {
            _logger = logger;
            _testService = testService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            TestHelper.A.Invoke();

            Action a = () =>
            {
                _testService!.Show();
            };
            a.Invoke();
            TestHelper.A = a;

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