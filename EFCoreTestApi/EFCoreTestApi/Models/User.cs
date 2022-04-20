﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreTestApi;

public abstract class EntityBase
{
    [Key]
    public virtual Guid Id { get; set; } = new Guid();
}

[Table("SysUser")]
public class User : EntityBase
{
    [MaxLength(128)]
    [Comment("用户名")]
    public string? Username { get; set; }

    [MaxLength(128)]
    [Comment("姓名")]
    public string? Name { get; set; }

    public virtual UserDetail? UserDetail { get; set; }
}

[Table("SysUserDetail")]
public class UserDetail : EntityBase
{
    [MaxLength(128)]
    [Comment("邮箱")]
    public string? Email { get; set; }

    [MaxLength(11)]
    [Comment("手机")]
    public string? Phone { get; set; }
}
