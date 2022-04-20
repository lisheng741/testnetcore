using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbFirstTest.Models
{
    [Table("Test")]
    public partial class Test
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(128)]
        public string? Name { get; set; }
    }
}
