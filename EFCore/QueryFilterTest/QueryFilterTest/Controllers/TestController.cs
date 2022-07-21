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
        // ���� IsDeleted == true ������
        var deletes = _context.Set<TestDelete>().ToList();

        // ����ɸѡ��������������
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