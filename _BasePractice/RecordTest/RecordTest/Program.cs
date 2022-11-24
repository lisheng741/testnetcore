// See https://aka.ms/new-console-template for more information
using System.Text.Json;

Console.WriteLine("Hello, World!");

var test = new TestModel("aaa");

Console.WriteLine(JsonSerializer.Serialize(test));

public record TestModel(string Name, string Txt = "1234");
