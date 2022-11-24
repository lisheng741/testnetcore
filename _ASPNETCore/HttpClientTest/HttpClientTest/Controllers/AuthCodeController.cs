using HttpClientTest.Code;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthCodeController : Controller
    {
        private readonly AuthCodeService _service;

        public AuthCodeController(AuthCodeService service)
        {
            _service= service;
        }

        [HttpGet]
        public async Task<string> GetCode()
        {
            await _service.GetAuthCodeAsync();
            return "";
        }

        [HttpGet]
        public async Task<string> Login(string code)
        {
            await _service.LoginAsync(code);
            return code;
        }

        [HttpGet]
        public async Task<string> GetInfo()
        {
            return await _service.GetInfoAsync();
        }
    }
}
