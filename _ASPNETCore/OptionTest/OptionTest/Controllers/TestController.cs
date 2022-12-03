using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OptionTest.Code;

namespace OptionTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly TestOptions _options;
        private readonly IOptionsMonitor<TestOptions> _optionsMonitor;

        public TestController(ILogger<TestController> logger, IOptions<TestOptions> options, IOptionsMonitor<TestOptions> optionsMonitor)
        {
            _logger = logger;
            _options = options.Value;
            _optionsMonitor = optionsMonitor;
        }

        [HttpGet]
        public string? Get()
        {
            Console.WriteLine(_options.Number);
            Console.WriteLine(_options.Text);

            var options1 = _optionsMonitor.Get("test1");
            var options2 = _optionsMonitor.Get("test2");

            Console.WriteLine(options1.Number);
            Console.WriteLine(options1.Text);
            Console.WriteLine(options2.Number);
            Console.WriteLine(options2.Text);

            return _options.Text;
        }
    }
}