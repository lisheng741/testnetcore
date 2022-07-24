using DataAnnotationTest.Model;
using Microsoft.AspNetCore.Mvc;

namespace DataAnnotationTest.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TestController : ControllerBase
{
    [HttpPost]
    public string TraditionValidation(TestModel model)
    {
        if (string.IsNullOrEmpty(model.Name))
        {
            return "名字不能为空！";
        }
        if (model.Name.Length > 10)
        {
            return "名字长度不能超过10个字符！";
        }

        return "验证通过！";
    }

    [HttpPost]
    public TestModel ModelValidation(TestModel model)
    {
        return model;
    }

    [HttpPost("List")]
    public List<TestModel> List(List<TestModel> models)
    {
        return models;
    }
}