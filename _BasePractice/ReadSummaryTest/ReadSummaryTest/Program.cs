using ReadSummaryTest.Models;
using System.Reflection;
using System.Xml;

Assembly assembly = Assembly.GetExecutingAssembly();

TestModel test = new();

var type = typeof(TestModel);

string summary = type.GetSummary();

// 属性
var fields = type.GetProperties();

var s1 = fields[0].GetSummary();
var s2 = fields[1].GetSummary();

// 方法
MethodInfo[] methodInfos = type.GetMethods();
var s3 = methodInfos[0].GetSummary();

// Field
FieldInfo[] fieldInfos = type.GetFields();
var s4 = fieldInfos[0].GetSummary();

// 成员
MemberInfo? memberInfo = type.GetMember(fields[0].Name).FirstOrDefault();

Console.ReadLine();
