using DataAnnotationTest.Model;
using Microsoft.AspNetCore.Mvc;

namespace DataAnnotationTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Get")]
        public TestModel Set(TestModel model)
        {
            return model;
        }
    }
}