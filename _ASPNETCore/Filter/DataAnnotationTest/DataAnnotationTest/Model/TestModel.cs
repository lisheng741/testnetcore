using System.ComponentModel.DataAnnotations;

namespace DataAnnotationTest.Model;

public class TestModel
{
    [Required(ErrorMessage = "名字不能为空！")]
    [StringLength(10, ErrorMessage = "名字长度不能超过10个字符！")]
    public string? Name { get; set; }

    [EmailAddress(ErrorMessage = "邮箱格式错误！")]
    public string? Email { get; set; }
}
