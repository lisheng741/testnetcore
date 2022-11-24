var builder = WebApplication.CreateBuilder(args);

Console.WriteLine(Path.GetTempFileName());

Parallel.For(0, 10, x =>
{
    //Console.WriteLine(Path.GetRandomFileName());
    var fileName = Path.GetRandomFileName();
    Console.WriteLine(fileName);
    Console.WriteLine(Path.GetFileNameWithoutExtension(fileName)); ;
});

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.Limits.MaxRequestBodySize = 256;
//});

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
