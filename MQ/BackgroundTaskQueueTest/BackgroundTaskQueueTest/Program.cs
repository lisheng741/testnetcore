using BackgroundTaskQueueTest.Code;
using BackgroundTaskQueueTest.EventBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//builder.Services.AddEventBusLocal().AddSubscriber(subscribers =>
//{
//    subscribers.Add<TestEventModel, TestErrorEventHandler>();
//    subscribers.Add<TestEventModel, TestEventHandler>();

//    subscribers.Add<Test2EventModel, Test22EventHandler>();
//    subscribers.Add<Test2EventModel, Test2EventHandler>();
//});

//builder.Services.AddEventBusRabbitMq(options =>
//{
//    options.HostName = "175.178.244.200";
//    options.UserName = "admin";
//    options.Password = "Qwe123456";
//})
//.AddSubscriber(subscribers =>
//{
//    subscribers.Add<TestEventModel, TestErrorEventHandler>();
//    subscribers.Add<TestEventModel, TestEventHandler>();

//    subscribers.Add<Test2EventModel, Test22EventHandler>();
//    subscribers.Add<Test2EventModel, Test2EventHandler>();
//});

builder.Services.AddEventBusRedis(options =>
{
    options.Configuration = "127.0.0.1:6379,password=123456";
})
.AddSubscriber(subscribers =>
{
    subscribers.Add<TestEventModel, TestErrorEventHandler>();
    subscribers.Add<TestEventModel, TestEventHandler>();

    subscribers.Add<Test2EventModel, Test22EventHandler>();
    subscribers.Add<Test2EventModel, Test2EventHandler>();
});



//// 官方示例：https://docs.microsoft.com/zh-cn/dotnet/core/extensions/queue-service?source=recommendations
//builder.Services.AddSingleton<MonitorLoop>();
//builder.Services.AddHostedService<QueuedHostedService>();
//builder.Services.AddSingleton<IBackgroundTaskQueue>(sp =>
//{
//    return new DefaultBackgroundTaskQueue(100);
//});


var app = builder.Build();

//// 官方示例：https://docs.microsoft.com/zh-cn/dotnet/core/extensions/queue-service?source=recommendations
//MonitorLoop monitorLoop = app.Services.GetRequiredService<MonitorLoop>()!;
//monitorLoop.StartMonitorLoop();

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
