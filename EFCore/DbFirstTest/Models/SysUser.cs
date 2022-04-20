using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbFirstTest.Models
{
    [Table("SysUser")]
    [Index("UserDetailId", Name = "IX_SysUser_UserDetailId")]
    public partial class SysUser
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [StringLength(128)]
        public string? Username { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(128)]
        public string? Name { get; set; }
        public Guid? UserDetailId { get; set; }

        [ForeignKey("UserDetailId")]
        [InverseProperty("SysUsers")]
        public virtual SysUserDetail? UserDetail { get; set; }
    }
}
