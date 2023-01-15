using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

ParameterExpression arg1Expr = Expression.Parameter(typeof(int), "arg1");
ParameterExpression arg2Expr = Expression.Parameter(typeof(int), "arg2");

var expr = Expression.Add(arg1Expr, arg2Expr);

//LambdaExpression lambdaExpr = Expression.Lambda(
//    expr,
//    new List<ParameterExpression>() { arg1Expr, arg2Expr }
//);

Expression<Func<int, int, int>> lambdaExpr = Expression.Lambda<Func<int, int, int>>(
    expr,
    new List<ParameterExpression>() { arg1Expr, arg2Expr }
);

// (arg1, arg2) => (arg1 + arg2)
Console.WriteLine(lambdaExpr);
var func = lambdaExpr.Compile();

// 3
Console.WriteLine(lambdaExpr.Compile().DynamicInvoke(1, 2));
Console.WriteLine(func(1, 2));


Test test = new Test();
Type type = test.GetType();
MemberExpression memberExpression = Expression.Property(Expression.Constant(test), "Id");

Console.WriteLine(memberExpression);

Console.WriteLine(memberExpression.Member.Name);

public class Test
{
    public int Id { get; set; }

    public string? Name { get; set; }
}
