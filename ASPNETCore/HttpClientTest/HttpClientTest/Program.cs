using HttpClientTest.Code;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddTransient<ISimpleAppService, SimpleAppService>();
builder.Services.AddHttpClient<ISimpleAppService, SimpleAppService>(client =>
{
    client.DefaultRequestHeaders.Add("add", "add value");
});
builder.Services.AddHttpClient<SimpleAppService>(client =>
{
    client.DefaultRequestHeaders.Add("add", "add value");
});

builder.Services.AddHttpClient<AuthCodeService>();

builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.PropertyNameCaseInsensitive = true;
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

app.Run();
