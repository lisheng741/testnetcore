using AuthenticationTest;
using AuthenticationTest.Authorization;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    //options.SwaggerDoc("v1");
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http
    });
    options.OperationFilter<AuthenticationOperationFilter>();
});

// 注册认证服务
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer("SimpleApp", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true, //是否验证Issuer
        ValidIssuer = "WebAppIssuer", //发行人Issuer
        ValidateAudience = true, //是否验证Audience
        ValidAudience = "WebAppAudience", //订阅人Audience
        ValidateIssuerSigningKey = true, //是否验证SecurityKey
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("8kh2luzmp0oq9wfbdeasygj647vr531n678fs")), //SecurityKey
        ValidateLifetime = true, //是否验证失效时间
        ClockSkew = TimeSpan.FromSeconds(300), //过期时间容错值，解决服务器端时间不同步问题（秒）
        RequireExpirationTime = true,
        NameClaimType = JwtClaimTypes.Name
    };
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true, //是否验证Issuer
        ValidIssuer = configuration["Jwt:Issuer"], //发行人Issuer
        ValidateAudience = true, //是否验证Audience
        ValidAudience = configuration["Jwt:Audience"], //订阅人Audience
        ValidateIssuerSigningKey = true, //是否验证SecurityKey
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])), //SecurityKey
        ValidateLifetime = true, //是否验证失效时间
        ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
        RequireExpirationTime = true,
        NameClaimType = JwtClaimTypes.Name
    };
    options.Events = new JwtBearerEvents()
    {
        OnChallenge = async context =>
        {
            context.Response.StatusCode = 401;
            // 报错
            await context.Response.WriteAsync("401");

            // 完成相应
            //context.Handled = true;
            context.HandleResponse();

            //return Task.CompletedTask;
        },
        OnForbidden = async context =>
        {
            //await context.Response.WriteAsync("403");
        }
    };
});



builder.Services.AddSingleton(new JwtHelper(configuration));

builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy(Permissions.UserCreate, policy => policy.AddRequirements(new PermissionAuthorizationRequirement(Permissions.UserCreate)));
//    options.AddPolicy(Permissions.UserUpdate, policy => policy.AddRequirements(new PermissionAuthorizationRequirement(Permissions.UserUpdate)));
//    options.AddPolicy(Permissions.UserDelete, policy => policy.AddRequirements(new PermissionAuthorizationRequirement(Permissions.UserDelete)));
//});
builder.Services.AddSingleton<IAuthorizationPolicyProvider, TestAuthorizationPolicyProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//调用 UseAuthentication（认证），必须在所有需要身份认证的中间件前调用，比如 UseAuthorization（授权）。
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
