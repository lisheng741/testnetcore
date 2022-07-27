using Castle.DynamicProxy;
using CastleApiTest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ProxyGenerator>();
builder.Services.AddTransient<TestServiceInterceptor>();


builder.Services.AddTransient<TestService>(provider =>
{
    var generator = provider.GetService<ProxyGenerator>();
    if (generator == null)
    {
        throw new ArgumentNullException(nameof(generator));
    }

    var interceptor = provider.GetService<TestServiceInterceptor>();
    var proxy = generator.CreateClassProxy<TestService>(interceptor);

    return proxy;
});


//builder.Services.AddTransient<TestService>();
//builder.Services.AddTransient<TestService>(provider =>
//{
//    var target = provider.GetService<TestService>();
//    if (target == null) return default!;

//    if (target.GetType() != typeof(TestService)) return target;

//    //if(target.GetType().Namespace == "Castle.Proxies")

//    var generator = provider.GetService<ProxyGenerator>();
//    if (generator == null)
//    {
//        throw new ArgumentNullException(nameof(generator));
//    }

//    var interceptor = provider.GetService<TestServiceInterceptor>();
//    var proxy = generator.CreateClassProxyWithTarget(target, interceptor);

//    return proxy;
//});


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
