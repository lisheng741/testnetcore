using AuthenticationTest.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationTest.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    [Authorize(Permissions.UserCreate)]
    public ActionResult<string> UserCreate() => "UserCreate";

    [HttpGet]
    [Authorize(Permissions.UserUpdate)]
    public ActionResult<string> UserUpdate() => "UserUpdate";

    [HttpGet]
    [Authorize(Permissions.UserDelete)]
    public ActionResult<string> UserDelete() => "UserDelete";
}
