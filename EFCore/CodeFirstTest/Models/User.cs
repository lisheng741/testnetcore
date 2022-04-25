using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstTest;

[Table("SysUser")]
public class User
{
    [Key]
    public Guid Id { get; set; } = new Guid();

    [StringLength(128)]
    [Comment("姓名")]
    public string? Name { get; set; }

    [StringLength(11)]
    [Comment("手机号码")]
    public string? Phone { get; set; }
}