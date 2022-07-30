using EFCoreTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple.Common.Guids;

namespace EFCoreTest.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class GuidController : ControllerBase
{
    private readonly EDbContext _context;

    public GuidController(EDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public List<TestGuid1> Get()
    {
        return _context.Set<TestGuid1>().ToList();
    }

    [HttpGet]
    public string Create(int begin = 0)
    {
        List<TestGuid1> list = new List<TestGuid1>();
        int end = begin + 1000;
        for (int i = begin; i < end; i++)
        {
            var guid = new TestGuid1()
            {
                Id = GuidHelper.Next(),
                //Id = GuidHelper.Create(),
                //Id = Guid.NewGuid(),
                CreateSequential = i
            };
            list.Add(guid);
        }

        _context.Set<TestGuid1>().AddRange(list);
        _context.SaveChanges();

        return "1";
    }
}
