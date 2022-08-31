
CancellationTokenSource cts = new CancellationTokenSource(1000);
Exec(cts.Token);

Console.WriteLine("取消前");
cts.Cancel();
Console.WriteLine("取消后");

await Task.Delay(2000);
Console.WriteLine("延时2秒后");

static async Task Exec(CancellationToken token = default)
{
    await Task.Delay(5000, token);

    Console.WriteLine("Exec完成");
}


var builder = WebApplication.CreateBuilder(args);

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
