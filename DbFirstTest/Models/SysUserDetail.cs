using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbFirstTest.Models
{
    [Table("SysUserDetail")]
    public partial class SysUserDetail
    {
        public SysUserDetail()
        {
            SysUsers = new HashSet<SysUser>();
        }

        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(128)]
        public string? Email { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [StringLength(11)]
        public string? Phone { get; set; }

        [InverseProperty("UserDetail")]
        public virtual ICollection<SysUser> SysUsers { get; set; }
    }
}
