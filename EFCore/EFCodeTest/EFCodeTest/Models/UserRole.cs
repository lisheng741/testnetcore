using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCodeTest.Models;

[Table("UserRole")]
public class UserRole
{
    [Key]
    public Guid UserId { get; set; }
    [Key]
    public Guid RoleId { get; set; }
    public User User { get; set; }
    public Role Role { get; set; }
}
