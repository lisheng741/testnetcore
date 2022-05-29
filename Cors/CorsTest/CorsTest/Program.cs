var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var s = configuration["AllowCors:1"];
//var urls = configuration.GetValue<string[]>("AllowCors"); //这个不行
var urls = configuration.GetSection("AllowCors").Get<string []>();
var lst = configuration.GetSection("AllowCors").Get<IEnumerable<string>>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins(urls);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("default");

app.UseAuthorization();

app.MapControllers();

app.Run();
