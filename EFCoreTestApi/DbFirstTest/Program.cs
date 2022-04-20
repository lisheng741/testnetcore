// See https://aka.ms/new-console-template for more information
using DbFirstTest.Data;
using DbFirstTest.Models;

Console.WriteLine("Hello, World!");

// dotnet ef dbcontext scaffold "server=localhost;database=efcore;uid=sa;pwd=Qwe123456;" Microsoft.EntityFrameworkCore.SqlServer --context-dir Data --output-dir Models --data-annotations

SysUser sysUser = new SysUser()
{
    Id = new Guid(),
    Name ="Add2",
    UserDetail = new SysUserDetail()
    {
        Id=new Guid(),
        Email = "123@qq.com"
    },
};
EfCoreContext db = new EfCoreContext();
db.SysUsers.Add(sysUser);
db.SaveChanges();

Console.WriteLine("Add User Success!");
Console.ReadLine();
