using ActionWebApiTest.Code;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddTransient<TestService>();

builder.Services.AddScoped<TestService>();


var app = builder.Build();

//var testService = app.Services.GetService<TestService>();
//Action a = () =>
//{
//    testService!.Show();
//};
//a.Invoke();
//TestHelper.A = a;


using (var scope = app.Services.CreateScope())
{
    var testService = scope.ServiceProvider.GetService<TestService>();
    Action a = () =>
    {
        testService!.Show();
    };
    a.Invoke();

    Action b = () =>
    {
        Console.WriteLine("≤‚ ‘");
    };
    b.Invoke();

    static void C()
    {
        Console.WriteLine("≤‚ ‘CCC");
    }
    C();

    TestHelper.A = a;
}


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
