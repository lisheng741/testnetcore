using Microsoft.AspNetCore.Mvc;
using SQLiteTest.Data;
using SQLiteTest.Models;
using System.Text.Json;

namespace SQLiteTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly MyContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public int Set(string name)
        {
            _context.TestModel.Add(new Models.TestModel()
            {
                Name = name
            });
            return _context.SaveChanges();
        }

        [HttpGet]
        public TestModel? Get()
        {
            var model = _context.TestModel.FirstOrDefault();
            Console.WriteLine(JsonSerializer.Serialize(model));

            return model;
        }

        [HttpGet]
        public int SetUser(string name)
        {
            _context.UserModel.Add(new Models.UserModel()
            {
                Name = name
            });
            return _context.SaveChanges();
        }

        [HttpGet]
        public UserModel? GetUser()
        {
            var model = _context.UserModel.FirstOrDefault();
            Console.WriteLine(JsonSerializer.Serialize(model));

            return model;
        }
    }
}