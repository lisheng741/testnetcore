using AuthenticationTest.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationTest;

public class JwtHelper
{
    private readonly IConfiguration _configuration;

    public JwtHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(bool isSimple = false)
    {
        var issuer = isSimple ? "WebAppIssuer" : _configuration["Jwt:Issuer"];
        var audience = isSimple ? "WebAppAudience" : _configuration["Jwt:Audience"];
        var secret = isSimple ? "8kh2luzmp0oq9wfbdeasygj647vr531n678fs" : _configuration["Jwt:SecretKey"];

        // 1. 定义需要使用到的Claims
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "u_admin"), //HttpContext.User.Identity.Name
            new Claim(ClaimTypes.Role, "r_admin"), //HttpContext.User.IsInRole("r_admin")
            new Claim(JwtRegisteredClaimNames.Jti, "admin"),
            new Claim("Username", "Admin"),
            new Claim("Name", "超级管理员"),
            new Claim("Permission", Permissions.UserCreate),
            new Claim("Permission", Permissions.UserUpdate),
            new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, DateTime.Now.AddSeconds(30).ToString()),
        };

        // 2. 从 appsettings.json 中读取SecretKey
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        // 3. 选择加密算法
        var algorithm = SecurityAlgorithms.HmacSha256;

        // 4. 生成Credentials
        var signingCredentials = new SigningCredentials(secretKey, algorithm);

        // 5. 根据以上，生成token
        var jwtSecurityToken = new JwtSecurityToken(
            issuer,     //Issuer
            audience,   //Audience
            claims,                          //Claims,
            DateTime.Now,                    //notBefore
            DateTime.Now.AddSeconds(30),    //expires
            signingCredentials: signingCredentials               //Credentials
        );

        // 6. 将token变为string
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return token;
    }
}