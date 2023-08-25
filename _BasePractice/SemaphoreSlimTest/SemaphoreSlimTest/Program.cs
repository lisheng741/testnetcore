using SemaphoreSlimTest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MapGet("/weatherforecast", () =>
{
    return "test";
})
.WithName("GetWeatherForecast");

Parallel.For(0, 5, async i =>
{
    var test = new Test();
    await test.Show(i.ToString());
});

System.Console.WriteLine("=================");

var semaphoreSlim = new SemaphoreSlim(1);

for (var i = 0; i < 6; i++)
{
    Pass(i);
}

Parallel.For(0, 5, i =>
{
    Thread.Sleep(500);
    PassByThread(i);
});

app.Run();

void Pass(int i)
{
    Task.Run(async () =>
    {
        await Task.Delay(500);
        try
        {
            await semaphoreSlim.WaitAsync();
            Console.WriteLine($"{i}正在通过");
            await Task.Delay(5000);
        }
        finally
        {
            Console.WriteLine($"{i}通过");
            semaphoreSlim.Release();
        }
    });
}

void PassByThread(int i)
{

    try
    {
        semaphoreSlim.Wait();
        Console.WriteLine($"{i}正在通过");
        Thread.Sleep(5000);
    }
    finally
    {
        Console.WriteLine($"{i}通过");
        semaphoreSlim.Release();
    }
}
