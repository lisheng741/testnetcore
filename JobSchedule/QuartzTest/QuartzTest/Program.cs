using Quartz;
using QuartzTest.Jobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddQuartz(config =>
{
    config.UseMicrosoftDependencyInjectionJobFactory();

    config.AddJob<TestJob>(options => options.WithIdentity("Test"));
    config.AddTrigger(options =>
        options.ForJob("Test")
        .WithIdentity("Test-Trigger")
        .WithCronSchedule("0/5 * * * * ?")
    ); 
});

// Quartz.Extensions.Hosting allows you to fire background service that handles scheduler lifecycle
builder.Services.AddQuartzHostedService(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();