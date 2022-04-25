using Microsoft.AspNetCore.Mvc;

namespace CodeFirstTest.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly TestContext _db;

    public UserController(TestContext db)
    {
        _db = db;
    }

    [HttpGet]
    public User? Get(Guid id)
    {
        return _db.User.Find(id);
    }

    [HttpPost]
    public void Post(User user)
    {
        _db.User.Add(user);
        _db.SaveChanges();
    }

    [HttpDelete]
    public bool Delete(Guid id)
    {
        User? user = _db.User.Find(id) ?? null;
        if (user == null) return false;
        _db.User.Remove(user);
        _db.SaveChanges();
        return true;
    }
}
