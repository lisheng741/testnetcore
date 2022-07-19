using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadSummaryTest.Models;

/// <summary>
/// 测试模型
/// </summary>
public class TestModel
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 名字
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// 内部id
    /// </summary>
    private int _id;

    /// <summary>
    /// 内部名称
    /// </summary>
    public string name = "";

    /// <summary>
    /// 测试方法
    /// </summary>
    public void TestMethod() { }
}
