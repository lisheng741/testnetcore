using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConcurrentTest.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly EDbContext _db;

    public TestController(EDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public string Set(TestConcurrent test)
    {
        _db.TestConcurrent.Add(test);
        _db.SaveChanges();
        return "1";
    }

    [HttpPost]
    public string Modify(TestConcurrent test)
    {
        Task.Delay(5000).Wait();
        try
        {
            _db.TestConcurrent.Update(test);
            _db.SaveChanges();
        }
        catch(DbUpdateException ex)
        {
            if(ex is DbUpdateConcurrencyException)
            {
                return "2";
            }
            Console.WriteLine(ex.Message);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return "1";
    }
}
