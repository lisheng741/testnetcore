using System;
using System.Linq.Expressions;

namespace ExpressionCombineTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var parameter = Expression.Parameter(typeof(int), "n");

            Expression<Func<int, int>> expr1 = n => (n + 1);
            Expression<Func<int, int>> expr2 = n => (n + 2);

            var visitor1 = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = visitor1.Visit(expr1.Body);
            var visitor2 = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = visitor2.Visit(expr2.Body);

            var expr = Expression.Lambda<Func<int, int>>(Expression.Add(left, right), parameter);
            var func = expr.Compile();

            Console.WriteLine(func(1));

            ///////////////////////

            var visitor = new TestExpressionVisitor();
            var left0 = visitor.Visit(expr1.Body);
            var right0 = visitor.Visit(expr2.Body); 
            
            var expr0 = Expression.Lambda<Func<int, int>>(Expression.Add(left0, right0), parameter);
            var func1 = expr0.Compile();

            Console.WriteLine(func1(1));

            Console.WriteLine("Hello World!");
        }
    }

    class TestExpressionVisitor : ExpressionVisitor
    {
        public override Expression Visit(Expression? node)
        {
            return base.Visit(node);
        }
    }
    class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression Visit(Expression? node)
        {
            if (node == _oldValue)
            {
                return _newValue;
            }

            return base.Visit(node)!;
        }
    }
}
