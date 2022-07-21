using EFCoreTest;
using EFCoreTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace QueryFilterTest.Controllers;

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
    public List<TestDelete> GetDeleteBase()
    {
        // 过滤 IsDeleted == true 的数据
        var deletes = _context.Set<TestDelete>().ToList();

        // 忽略筛选器，不过滤数据
        var allDeletes = _context.Set<TestDelete>().IgnoreQueryFilters().ToList();

        return allDeletes;
    }

    [HttpGet]
    public List<TestTenant> GetTenant()
    {
        var tenants = _context.Set<TestTenant>().ToList();

        var allTenants = _context.Set<TestTenant>().IgnoreQueryFilters().ToList();

        return tenants;
    }

    [HttpGet]
    public List<TestTenant> GetTenantIgnoreDelete()
    {
        _context.IgnoreDeleteFilter = true;

        var tenants = _context.Set<TestTenant>().ToList();

        var allTenants = _context.Set<TestTenant>().IgnoreQueryFilters().ToList();

        return tenants;
    }
}