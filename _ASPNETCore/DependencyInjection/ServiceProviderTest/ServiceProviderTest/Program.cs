using Microsoft.Extensions.Options;
using ServiceProviderTest.Servies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(o =>
{
    Console.WriteLine("c");
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<TestService>();

var serviceProvider = builder.Services.BuildServiceProvider();

var service = serviceProvider.GetService<TestService>();

Console.WriteLine(service.Get());

builder.Services.AddTransient<Test2Service>();

//var service2 = serviceProvider.GetService<Test2Service>();

//Console.WriteLine(service2.Get());



var serviceProvider2 = builder.Services.BuildServiceProvider();

var service1 = serviceProvider2.GetService<TestService>();

Console.WriteLine(service1.Get());

var service2 = serviceProvider2.GetService<Test2Service>();

Console.WriteLine(service2.Get());


builder.Services.Configure<TestService>(service =>
{
    Console.WriteLine($"{DateTime.Now}  {service.Get()}");
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

Console.WriteLine(app.Services.GetService<IOptions<TestService>>().Value) ;

app.Run();
