using System.ComponentModel.DataAnnotations;

namespace CascadDeleteTest.Models;


public class User : EntityBase<Guid>
{
    public string Name { get; set; }

    /// <summary>
    /// 用户角色
    /// </summary>
    public UserRole UserRole { get; set; }
}

public class Role : EntityBase<Guid>
{
    public string Name { get; set; }

    /// <summary>
    /// 用户角色
    /// </summary>
    public UserRole UserRole { get; set; }

}
public class UserRole : EntityBase
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    /// <summary>
    /// 用户
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    public Role Role { get; set; }

    public override void ConfigureEntity(ModelBuilder builder)
    {
        builder.Entity<UserRole>().HasKey(e => new { e.UserId, e.RoleId });
    }
}
