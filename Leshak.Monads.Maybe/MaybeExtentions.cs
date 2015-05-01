using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leshak.Monads.Maybe
{
    public static class MaybeExtentions
    {
        public static TResult With<TInput, TResult>(this TInput input, Func<TInput, TResult> evaluator)
            where TInput : class
            where TResult : class
        {
            if (input == null) return null;
            else return evaluator(input);
        }


        public static TResult Return<TInput, TResult>(this TInput input, Func<TInput, TResult> evaluator,TResult resultOnNullInChain)
            where TInput : class
            where TResult : class
        {
            if (input == null) return resultOnNullInChain;
            else return evaluator(input);
        }
    }
}
