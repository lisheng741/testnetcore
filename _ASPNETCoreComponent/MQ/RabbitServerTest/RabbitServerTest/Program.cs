using RabbitMQ.Client;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
//
// https://www.cnblogs.com/stulzq/p/7551819.html
// 

var config = builder.Configuration;
ConnectionFactory factory = new ConnectionFactory()
{
    HostName = config["RabbitMQ:Connection"],
    UserName = config["RabbitMQ:UserName"],
    Password = config["RabbitMQ:Password"],
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// worker 模式
//channel.QueueDeclare("test_queue", false, false, false, null);
// 发布/订阅模式
//channel.ExchangeDeclare("test_exchange_fanout", "fanout");
//channel.QueueBind("test_queue_1", "test_exchange_fanout", "");
//channel.QueueBind("test_queue_2", "test_exchange_fanout", "");


channel.ExchangeDeclare("test_exchange", "direct");

Console.WriteLine("启动Server");
string? input;
while (true)
{
    input = Console.ReadLine();
    if (string.IsNullOrEmpty(input))
    {
        channel.Close();
        connection.Close();
        break;
    }

    var sendBytes = Encoding.UTF8.GetBytes(input);
    //channel.BasicPublish("", "test_queue", null, sendBytes);
    channel.BasicPublish("test_exchange", "key", null, sendBytes);
}
Console.WriteLine("结束");

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
