using EFCoreTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTest.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TestController : ControllerBase
{
    private readonly EDbContext _dbContext;
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger, EDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    public List<TestDelete> GetDelete(bool ignoreDele = false)
    {
        IQueryable<TestDelete> queryable = _dbContext.Set<TestDelete>();

        queryable = queryable.Where(e => true);

        if (ignoreDele)
        {
            queryable = queryable.IgnoreQueryFilters();
        }

        List<TestDelete> result = queryable.ToList();

        return result;
    }

    [HttpPost]
    public void SetDelete(TestDelete testDelete)
    {
        _dbContext.Set<TestDelete>().Add(testDelete);

        _dbContext.Add(testDelete);
        _dbContext.SaveChanges();
    }
}