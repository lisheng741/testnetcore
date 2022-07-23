using EFCoreTest;
using EFCoreTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChangeTrackerTest.Controllers;

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
    public void Inert()
    {
        TestBusiness test = new TestBusiness() { Name = "测试" };
        _context.Set<TestBusiness>().Add(test);
        _context.SaveChanges();
    }

    [HttpGet]
    public void Update()
    {
        TestBusiness? test = _context.Set<TestBusiness>().FirstOrDefault();
        if (test == null) return;
        test.Name = "new name";
        _context.Set<TestBusiness>().Update(test); // 这一句加不加好像无所谓
        _context.SaveChanges();
    }

    [HttpGet]
    public void Delete()
    {
        TestBusiness? test = _context.Set<TestBusiness>().FirstOrDefault(t => !t.IsDeleted);
        if (test == null) return;
        _context.Set<TestBusiness>().Remove(test);
        _context.SaveChanges();
    }

    [HttpGet]
    public void HardDelete()
    {
        TestBusiness? test = _context.Set<TestBusiness>().FirstOrDefault(t => !t.IsDeleted);
        if (test == null) return;
        _context.IgnoreDeleteFilter = true;
        _context.Set<TestBusiness>().Remove(test);
        _context.SaveChanges();
    }

    [HttpGet]
    public void RemoveRange()
    {
        List<TestDelete> deletes = new List<TestDelete>()
        {
            new TestDelete(){Name = "删1"},
            new TestDelete(){Name = "删2"},
            new TestDelete(){Name = "删3"},
        };
        _context.Set<TestDelete>().AddRange(deletes);
        _context.SaveChanges();


        _context.Set<TestDelete>().RemoveRange(deletes);
        _context.SaveChanges();
    }

    [HttpGet]
    public void Test()
    {
        var dele = _context.Set<TestTenant>().Find(3);

        _context.Set<TestTenant>().Remove(dele);

        _context.Set<TestTenant>().Remove(new TestTenant() { Id = 5 });
        _context.SaveChanges();

        _context.Set<TestTenant>().Update(new TestTenant() { Id = 2 });


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