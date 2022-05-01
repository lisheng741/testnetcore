using EFCodeTest.Data;
using EFCodeTest.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCodeTest.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly EDbContext _db;

    public UserController(EDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public User? Get(Guid id)
    {
        User? user = _db.User.Find(id);
        return user;
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
