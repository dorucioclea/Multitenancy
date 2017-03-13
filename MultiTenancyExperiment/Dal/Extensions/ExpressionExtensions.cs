using System;
using System.Linq.Expressions;

namespace MultiTenancyExperiment.Dal.Extensions
{
    public static class ExpressionExtensions
    {
        public static string GetName<TSource, TField>(this Expression<Func<TSource, TField>> field)
        {
            if (Equals(field, null))
            {
                throw new NullReferenceException("Field is required");
            }

            MemberExpression expr;

            var body = field.Body as MemberExpression;
            if (body != null)
            {
                expr = body;
            }
            else
            {
                var expression = field.Body as UnaryExpression;
                if (expression != null)
                {
                    expr = (MemberExpression)expression.Operand;
                }
                else
                {
                    const string format = "Expression '{0}' not supported.";
                    var message = string.Format(format, field);
                    throw new ArgumentException(message, "field");
                }
            }

            return expr.Member.Name;
        }
    }
}
