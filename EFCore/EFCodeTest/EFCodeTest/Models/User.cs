using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCodeTest.Models
{
    [Table("SysUser")]
    public class User : IEntityBase<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(128)]
        [Comment("姓名")]
        public string Name { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual UserDetail? UserDetail { get; set; }
    }

    public class UserDetail : IEntityBase<Guid>
    {
        public Guid Id { get; set; }

        [StringLength(11, MinimumLength =11)]
        [Comment("手机号码")]
        public string? Phone { get; set; }

        public virtual Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}
