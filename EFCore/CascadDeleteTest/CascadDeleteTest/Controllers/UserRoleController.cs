using CascadDeleteTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CascadDeleteTest.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserRoleController : ControllerBase
{
    private readonly SimpleDbContext _context;

    public UserRoleController(SimpleDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public void Set(int n)
    {
        User user = new User()
        {
            Name = $"用户名{n}",
            UserRole = new UserRole()
            {
                Role = new Role()
                {
                    Name = $"角色名{n}"
                }
            }
        };

        _context.Add(user);
        _context.SaveChanges();
    }

    [HttpGet]
    public void DeleteUser(Guid id)
    {
        User? user = _context.Set<User>().FirstOrDefault(u => u.Id == id);
        if (user == null) return;

        _context.Remove(user);
        _context.SaveChanges();
    }

    [HttpGet]
    public void DeleteRole(Guid id)
    {
        Role? role = _context.Set<Role>().FirstOrDefault(r => r.Id == id);
        if (role == null) return;

        _context.Remove(role);
        _context.SaveChanges();
    }

    [HttpGet]
    public void DeleteUserRole(Guid id)
    {
        User? user = _context.Set<User>()
            .Include(u => u.UserRole)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefault(u => u.Id == id);
        if (user == null) return;

        _context.Remove(user.UserRole.Role);
        _context.SaveChanges();
    }
}
