
using ConstructorTest;
using Simple.Common.Helpers;

Child child = new Child();

var test0 = ActivatorHelper.CreateInstance(typeof(TestClass0));
var test1 = ActivatorHelper.CreateInstance(typeof(TestClass1));
var test2 = ActivatorHelper.CreateInstance(typeof(TestClass2));
var test3 = ActivatorHelper.CreateInstance(typeof(TestClass3));

var test = Activator.CreateInstance(typeof(TestClassPrivate));
var testPrivate = ActivatorHelper.CreateInstance(typeof(TestClassPrivate));

Console.ReadLine();

public class Child : Parent
{
	public Child()
		: base("child set")
	{
		CName = "child";
	}

	public string CName { get; set; }
}

public class Parent
{
	public Parent()
	{
		Name = "parent";
	}

	public Parent(string name)
	{
		Name = name;
	}

	public string Name { get; set; }
}
