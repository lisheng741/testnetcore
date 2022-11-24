using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using AspectCoreApiTest;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.ConfigureDynamicProxy(config =>
{
    //config.Interceptors.AddTyped<TestInterceptorAttribute>(method =>
    //{
    //    return method.Name.EndsWith("Service");
    //});

    config.Interceptors.AddTyped<TestInterceptorAttribute>();
});

builder.Services.AddTransient<ITestService, TestService>();
builder.Services.AddTransient<TestService>();
builder.Services.AddTransient<Test2Service>();

var app = builder.Build();

ITestService test = new TestService();

test.Show();

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
