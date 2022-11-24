using Microsoft.EntityFrameworkCore;
using SQLiteTest.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlPath = Path.Combine(AppContext.BaseDirectory, "my.db");
var sqlConnectString = $"Filename={sqlPath}";
Console.WriteLine(sqlConnectString);
builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseSqlite(sqlConnectString);
});

var app = builder.Build();

// 在运行中迁移
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetService<MyContext>();
db!.Database.Migrate();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
