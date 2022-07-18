using System.ComponentModel.DataAnnotations;

namespace DataAnnotationTest.Model;

public class TestModel
{
    [Required(ErrorMessage = "{0}是必须的！")]
    public int? Id { get; set; }

    [Required]
    [StringLength(10, ErrorMessage = "{0}字段长度为{1}！")]
    public string? Name { get; set; }

    [Phone(ErrorMessage = "{0}为手机格式！")]
    public string? Phone { get; set; }
}
