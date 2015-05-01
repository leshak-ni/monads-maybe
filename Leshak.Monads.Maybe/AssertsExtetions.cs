using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leshak.Monads.Maybe
{
    public static class AssertsExtetions
    {
        /// <summary>
        ///  Context will change, pass true if input is null
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNull<TInput>(this TInput input) // in screencast called ReturnSuccess
        {
            return input == null;
        }

        /// <summary>
        ///  Context will change, pass true if input is not null
        /// </summary>
        public static bool IsNotNull<TInput>(this TInput input)
        {
            return input != null;
        }


        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }

        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        public static bool IsNullOrEmptyOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input) || input == string.Empty;
        }


    }
}
