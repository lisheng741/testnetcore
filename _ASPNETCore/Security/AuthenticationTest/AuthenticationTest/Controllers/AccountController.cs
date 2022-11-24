using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationTest.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly JwtHelper _jwtHelper;

    public AccountController(JwtHelper jwtHelper)
    {
        _jwtHelper = jwtHelper;
    }

    [HttpGet]
    public ActionResult<string> GetSimpleToken()
    {
        return _jwtHelper.CreateToken(true);
    }

    [HttpGet]
    public ActionResult<string> GetToken()
    {
        return _jwtHelper.CreateToken();
    }

    [Authorize]
    [HttpGet]
    public ActionResult<string> GetTest()
    {
        Console.WriteLine(HttpContext.User.Identity.Name);
        return "Test Authorize";
    }
}
