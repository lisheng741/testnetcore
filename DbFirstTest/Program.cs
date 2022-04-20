// See https://aka.ms/new-console-template for more information
using DbFirstTest.Data;
using DbFirstTest.Models;

Console.WriteLine("Hello, World!");

EfCoreContext db = new EfCoreContext();
Test test = new Test()
{
    Id = Guid.NewGuid(),
    Name = "Add"
};
db.Tests.Add(test);
db.SaveChanges();

Console.WriteLine("Add Successful！");