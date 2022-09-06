using Pipelines.Sockets.Unofficial;
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

Action action = () =>
{
    Console.WriteLine("1111");
};
action += () =>
{
    Console.WriteLine("22222");
};
action.Invoke();

List<Action> acts = action.AsEnumerable().ToList();

var config = builder.Configuration;
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(config["Redis:ConnectionString"]);
var subscriber = redis.GetSubscriber();

string? input;
while (true)
{
    input = Console.ReadLine();
    if (string.IsNullOrEmpty(input))
    {
        break;
    }
    subscriber.Publish("test", input);
}

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
