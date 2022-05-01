using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EFCodeTest.Models
{
    [Table("SysRole")]
    public class Role : IEntityBase<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(64)]
        [Comment("角色名")]
        public string Name { get; set; }

        public virtual User? User { get; set; }
    }
}
