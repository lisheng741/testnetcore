using OptionTest.Code;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<TestOptions>("test1", options =>
{
    options.Number = 1;
    options.Text = "test1";
});
builder.Services.Configure<TestOptions>("test1", options =>
{
    options.Number = 11;
    options.Text = "test11";
});
builder.Services.Configure<TestOptions>("test2", options =>
{
    options.Number = 2;
    options.Text = "test2";
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
