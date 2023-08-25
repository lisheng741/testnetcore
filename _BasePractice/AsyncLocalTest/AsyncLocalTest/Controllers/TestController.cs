using Microsoft.AspNetCore.Mvc;

namespace AsyncLocalTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private static readonly AsyncLocal<string> _stringCurrent = new AsyncLocal<string>();
        private static string _stringStatic = "";
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<string> Get(string value)
        {
            _stringStatic = value;
            _stringCurrent.Value = value;

            var result = $"before == Id1={Thread.CurrentThread.ManagedThreadId} -- {_stringCurrent.Value} -- {_stringStatic}\n";

            await Task.Delay(6000);

            result += $"after  == Id2={Thread.CurrentThread.ManagedThreadId} -- {_stringCurrent.Value} -- {_stringStatic}";

            return result;
        }
    }
}