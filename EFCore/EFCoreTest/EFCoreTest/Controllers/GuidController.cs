using EFCoreTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple.Common.Guids;
using Volo.Abp.Guids;

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
        //return _context.Set<TestGuid1>().ToList().OrderBy(g => g.Id).ToList();
        return _context.Set<TestGuid1>().OrderBy(g => g.Id).ToList();
    }

    [HttpGet]
    public string Create(int begin = 0)
    {
        List<TestGuid> list = new List<TestGuid>();
        int end = begin + 1000;
        for (int i = begin; i < end; i++)
        {
            var guid = new TestGuid()
            {
                Id = SequentialGuidGenerator.Create(SequentialGuidType.AtEnd),
                //Id = GuidHelper.NextOld(SequentialGuidType.AtEnd),
                //Id = GuidHelper.Create(),
                //Id = Guid.NewGuid(),
                CreateSequential = i
            };
            list.Add(guid);
        }

        _context.Set<TestGuid>().AddRange(list);
        _context.SaveChanges();

        return "0";
    }

    [HttpGet]
    public string Create1(int begin = 0)
    {
        List<TestGuid1> list = new List<TestGuid1>();
        int end = begin + 1000;
        for (int i = begin; i < end; i++)
        {
            var guid = new TestGuid1()
            {
                Id = SequentialGuidGenerator.Create(SequentialGuidType.AsString),
                //Id = GuidHelper.Next(),
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
