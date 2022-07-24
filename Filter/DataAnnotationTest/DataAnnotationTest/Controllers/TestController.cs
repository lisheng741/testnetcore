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
            return "���ֲ���Ϊ�գ�";
        }
        if (model.Name.Length > 10)
        {
            return "���ֳ��Ȳ��ܳ���10���ַ���";
        }

        return "��֤ͨ����";
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