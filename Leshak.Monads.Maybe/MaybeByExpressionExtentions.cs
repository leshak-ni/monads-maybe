using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;

namespace Leshak.Monads.Maybe
{
    public static class MaybeByExpressionExtentions
    {
        /// <summary>
        /// Returns the value of an expression, or <c>default(T)</c> if any parts of the expression are <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of the Expression</typeparam>
        /// <param name="expression">A parameterless lambda representing the path to the value.</param>
        /// <returns>The value of the expression, or <c>default(T)</c> if any parts of the expression are <c>null</c>.</returns>
          public static T With<T>(Expression<Func<T>> expression)
        {
            throw new NotImplementedException();
            var value = Maybe(expression.Body);
            if (value == null) return default(T);
            return (T)value;
        }

        #region Private
        private static object Maybe(Expression expression)
        {
            var constantExpression = expression as ConstantExpression;
            if (constantExpression != null)
            {
                return constantExpression.Value;
            }

            var memberExpression = expression as MemberExpression;
            if (memberExpression != null)
            {
                var memberValue = Maybe(memberExpression.Expression);
                if (memberValue != null)
                {
                    var member = memberExpression.Member;
                    return GetValue(member, memberValue);
                }
            }

            return null;
        }

        private static object GetValue(MemberInfo member, object memberValue)
        {
            var propertyInfo = member as PropertyInfo;
            if (propertyInfo != null) return propertyInfo.GetValue(memberValue, null);

            var fieldInfo = member as FieldInfo;
            if (fieldInfo != null) return fieldInfo.GetValue(memberValue);

            return null;
        }
        #endregion
    }
}
