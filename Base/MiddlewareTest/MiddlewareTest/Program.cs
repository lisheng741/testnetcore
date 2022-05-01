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

app.Use(async(context, next) =>
{
    Console.WriteLine("Use 1 input");
    await next.Invoke();
    Console.WriteLine("Use 1 output");
});

app.Use(async (context, next) =>
{
    Console.WriteLine("Use 2 input");
    await next.Invoke(context);
    Console.WriteLine("Use 2 output");
});

app.Map("/test", app =>
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("test map");
    });
});

app.Run();