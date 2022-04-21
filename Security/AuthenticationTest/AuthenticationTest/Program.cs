using AuthenticationTest;
using AuthenticationTest.Authorization;
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

// ע����֤����
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true, //�Ƿ���֤Issuer
        ValidIssuer = configuration["Jwt:Issuer"], //������Issuer
        ValidateAudience = true, //�Ƿ���֤Audience
        ValidAudience = configuration["Jwt:Audience"], //������Audience
        ValidateIssuerSigningKey = true, //�Ƿ���֤SecurityKey
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])), //SecurityKey
        ValidateLifetime = true, //�Ƿ���֤ʧЧʱ��
        ClockSkew = TimeSpan.FromSeconds(30), //����ʱ���ݴ�ֵ�������������ʱ�䲻ͬ�����⣨�룩
        RequireExpirationTime = true,
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

//���� UseAuthentication����֤����������������Ҫ�����֤���м��ǰ���ã����� UseAuthorization����Ȩ����
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
