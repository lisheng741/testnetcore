using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwaggerTest;

public class AuthenticationOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authorizeAttributes = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .Distinct();

        if (authorizeAttributes.Any())
        {
            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });
            var bearerScheme = new OpenApiSecurityScheme
            {
                Scheme = "Bearer",
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            };
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement()
                {
                    [ bearerScheme ] = Array.Empty<string>()
                }
            };
        }
    }
}

//public class AuthenticationOperationFilter : IOperationFilter
//{
//    public void Apply(OpenApiOperation operation, OperationFilterContext context)
//    {
//        var actionScopes = context.MethodInfo.GetCustomAttributes(true)
//            .OfType<AuthorizeAttribute>()
//            .Select(attr => attr.TypeId.ToString())
//            .Distinct();
//        var controllerScopes = context.MethodInfo.DeclaringType!.GetCustomAttributes(true)
//            .Union(context.MethodInfo.GetCustomAttributes(true))
//            .OfType<AuthorizeAttribute>()
//            .Select(attr => attr.TypeId.ToString());
//        var requiredScopes = actionScopes.Union(controllerScopes).Distinct().ToArray();

//        if (requiredScopes.Any())
//        {
//            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
//            //operation.Responses.Add("419", new OpenApiResponse { Description = "AuthenticationTimeout" });
//            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });
//            var oAuthScheme = new OpenApiSecurityScheme
//            {
//                Scheme = "Bearer",
//                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
//            };
//            operation.Security = new List<OpenApiSecurityRequirement>
//            {
//                new OpenApiSecurityRequirement
//                {
//                    [ oAuthScheme ] = new List<string>()
//                }
//            };
//        }
//    }
//}
