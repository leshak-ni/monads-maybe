using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;

namespace Leshak.Monads.Maybe
{
    public static class MaybeByExpressionExtensions
    {
        /// <summary>
        /// Returns the value of an expression, or <c>default(T)</c> if any parts of the expression are <c>null</c>.
        /// it's like MaybeExtentions.With but evaluator is expression tree
        /// </summary>
        /// <typeparam name="TInput">The type of the Expression</typeparam>
        /// <param name="expression">A parameterless lambda representing the path to the value.</param>
        /// <returns>The value of the expression, or <c>default(TResult)</c> if any parts of the expression are <c>null</c>.</returns>
        public static TResult Maybe<TInput, TResult>(this TInput input, Expression<Func<TInput,TResult>> expression)
        {
            var value = Maybe(expression.Body,input);
            
            if (value == null) return default(TResult);
            return (TResult)value;
        }

        public static T Maybe<T>(Expression<Func<T>> expression) // original implementation
        {
            var value = Maybe(expression.Body);
            if (value == null) return default(T);
            return (T)value;
        }

        #region Private
        private static object Maybe(Expression expression,object input=null)
        {
            var constantExpression = expression as ConstantExpression;
            if (constantExpression != null)
            {
                return constantExpression.Value;
            }

            var paramExpression = expression as ParameterExpression;
            if (paramExpression != null)
            {
                return input; //hack: input is our parametr, we no need refrection to get it value
            }

            var memberExpression = expression as MemberExpression;
            if (memberExpression != null)
            {
                var memberValue = Maybe(memberExpression.Expression,input);
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
