using EFCoreTest;
using Microsoft.EntityFrameworkCore;

string guid = Guid.NewGuid().ToString("N");

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EDbContext>(options => {
    //options.UseSqlServer(configuration["ConnectionStrings:SqlServer"]);
    options.UseMySql(configuration["ConnectionStrings:MySql"], ServerVersion.Parse("8.0.26"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
