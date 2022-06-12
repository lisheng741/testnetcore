using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreRemoveRangeTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly EDbContext _db;

        public TestController(EDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public List<DeleteTest> AddRange()
        {
            var list = new List<DeleteTest>()
            {
                new DeleteTest(){Name = Guid.NewGuid().ToString()},
                new DeleteTest(){Name = Guid.NewGuid().ToString()},
                new DeleteTest(){Name = Guid.NewGuid().ToString()},
                new DeleteTest(){Name = Guid.NewGuid().ToString()},
                new DeleteTest(){Name = Guid.NewGuid().ToString()}
            };
            _db.AddRange(list);
            _db.SaveChanges();

            return list;
        }

        [HttpGet]
        public void RemoveRange(int min, int max)
        {
            var list = _db.DeleteTest.Where(t => t.Id >= min && t.Id <= max).ToList();
            _db.RemoveRange(list);
            _db.SaveChanges();
        }
    }
}
