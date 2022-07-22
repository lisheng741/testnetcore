using EFCoreTest;
using EFCoreTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChangeTrackerTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly EDbContext _context;
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger, EDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public void Test()
        {
            var tenant = _context.Set<TestTenant>().ToList();

            _context.Set<TestTenant>().Remove(tenant[2]);

            tenant[0].Name = "1234";

            TestTenant newEntity = new TestTenant()
            {
                Name = "new"
            };

            _context.Set<TestTenant>().Add(newEntity);
        }
    }
}