using BackgroundTaskQueueTest.Code;
using BackgroundTaskQueueTest.EventBus.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundTaskQueueTest.Controllers
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
        private readonly IEventPublisher _publisher;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IEventPublisher publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            TestEventModel @event = new TestEventModel();
            Console.WriteLine($"发布{@event.Id}");
            await _publisher.PublishAsync(@event);

            Test2EventModel @event2 = new Test2EventModel();
            Console.WriteLine($"发布{@event2.Id}");
            await _publisher.PublishAsync(@event2);

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