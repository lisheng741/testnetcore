using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//
// https://www.cnblogs.com/stulzq/p/7542012.html
//

var config = builder.Configuration;
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(config["Redis:ConnectionString"]);
var subscriber = redis.GetSubscriber();
subscriber.Subscribe("test", (channel, message) =>
{
    Console.WriteLine($"�յ���Ϣ��{message}");
});
Console.WriteLine("���� test channel");
Console.ReadLine();

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
