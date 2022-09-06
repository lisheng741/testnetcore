using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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

Console.WriteLine("启动");
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

//channel.ExchangeDeclare("test_exchange_fanout", "fanout");
//channel.QueueBind("test_queue_2", "test_exchange_fanout", "");

channel.ExchangeDeclare("test_exchange", "direct");
channel.QueueDeclare("test_queue_1", false, false, false, null);
channel.QueueBind("test_queue_2", "test_exchange", "key");
channel.BasicQos(0, 1, false);
EventingBasicConsumer consumer = new EventingBasicConsumer(channel); // 消费者
consumer.Received += (sender, e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());
    Console.WriteLine($"收到消息： {message}");
    channel.BasicAck(e.DeliveryTag, false); // 确认该消息已被消费
};

channel.BasicConsume("test_queue_2", false, consumer);
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
