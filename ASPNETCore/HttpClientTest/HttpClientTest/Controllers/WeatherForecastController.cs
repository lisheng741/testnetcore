using HttpClientTest.Code;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ISimpleAppService _simpleAppService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISimpleAppService simpleAppService)
        {
            _logger = logger;
            _simpleAppService = simpleAppService;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            await _simpleAppService.LoginAsync();
            return "";
        }
    }
}