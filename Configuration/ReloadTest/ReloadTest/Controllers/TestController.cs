using Microsoft.AspNetCore.Mvc;

namespace ReloadTest.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public string GetConfig()
        {
            return _configuration["Test"];
        }
    }
}