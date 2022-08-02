using Volo.Abp.Guids;

long timestamp = DateTime.UtcNow.Ticks;
long timestampHigh = timestamp / 10000L;
long timestampLow = timestamp % 10000L;

byte[] timestampHighBytes = BitConverter.GetBytes(timestampHigh);
byte[] timestampLowBytes = BitConverter.GetBytes(timestampLow);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IGuidGenerator, SequentialGuidGenerator>();

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
