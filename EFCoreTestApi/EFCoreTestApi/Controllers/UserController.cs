using Microsoft.AspNetCore.Mvc;

namespace EFCoreTestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly MySqlDbContext _db;

    public UserController(MySqlDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public ActionResult<User?> Get(Guid id)
    {
        User? user = _db.User.Find(id);
        return user;
    }

    [HttpPut]
    public ActionResult<bool> Put([FromBody]User user)
    {
        _db.User.Add(user);
        _db.SaveChanges();
        return true;
    }

    [HttpDelete]
    public ActionResult<bool> Delete(Guid id)
    {
        User user = _db.User.Find(id);
        _db.User.Remove(user);
        _db.SaveChanges();
        return true;
    }
}

