using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace RedisTest.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly IDistributedCache _cache;

    public TestController(IDistributedCache cache)
    {
        _cache = cache;
    }

    [HttpGet]
    public async Task Set(string key, string value, double expiration)
    {
        var encodedValue = Encoding.UTF8.GetBytes(value);
        await _cache.SetAsync(key, encodedValue,new DistributedCacheEntryOptions() { AbsoluteExpiration = DateTime.Now.AddSeconds(expiration) });
    }

    [HttpGet]
    public async Task<string?> Get(string key)
    {
        var encodedValue = await _cache.GetAsync(key);
        if (encodedValue == null) return null;
        return Encoding.UTF8.GetString(encodedValue);
    }
}
