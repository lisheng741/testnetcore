using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConcurrentTest.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RowVersionController : ControllerBase
{
    private readonly EDbContext _db;

    public RowVersionController(EDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public string Set(string name)
    {
        var test = new TestRowVersion() { Name = name};
        _db.Add(test);
        _db.SaveChanges();
        return "1";
    }

    [HttpPost]
    public TestRowVersion? Modify(int id,string name)
    {
        TestRowVersion test = _db.TestRowVersion.FirstOrDefault(r => r.Id == id);
        test.Name = name;

        Task.Delay(5000).Wait();
        try
        {
            _db.Update(test);
            _db.SaveChanges();
        }
        catch(DbUpdateException ex)
        {
            Console.WriteLine(ex.Message);
            if (ex is DbUpdateConcurrencyException)
            {
                Console.WriteLine("1");
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return test;
    }
}
