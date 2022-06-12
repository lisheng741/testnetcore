using AssemblyLib1Test;
using AssemblyLib2Test;
using AssemblyTest;
using AutoMapper;
using System.Runtime.Loader;

var test = new TestMapper();
var test2 = new InTestMapper();

var builder = WebApplication.CreateBuilder(args);

var assemblies1 = AssemblyLoadContext.Default.Assemblies.ToList();
var assemblies2 = AppDomain.CurrentDomain.GetAssemblies();

AssemblyHelper.GetAssemblies(new List<string>() { "AssemblyLibAutoTest" });
AssemblyHelper.GetAssemblies(new List<string>() { "AssemblyLibAutoTest" });
AssemblyHelper.GetAssemblies(new List<string>() { "AssemblyLibAutoTest" });

var assemblies11 = AssemblyLoadContext.Default.Assemblies.ToList();
var assemblies12 = AppDomain.CurrentDomain.GetAssemblies();

var lst = assemblies12.Where(a => a.GetTypes()
        .Where(t => t.IsSubclassOf(typeof(Profile)))
        .Any()
    ).ToList();

//builder.Services.AddAutoMapper();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
