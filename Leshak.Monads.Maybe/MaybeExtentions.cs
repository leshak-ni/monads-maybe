﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leshak.Monads.Maybe
{
    public static class MaybeExtentions
    {
        public static TResult With<TInput, TResult>(this TInput input, Func<TInput, TResult> evaluator)
        {
            if (input == null) return default(TResult);
            else return evaluator(input);
        }


        public static TResult Return<TInput, TResult>(this TInput input, Func<TInput, TResult> evaluator,TResult resultOnNullInChain)
        {
            if (input == null) return resultOnNullInChain;
            else return evaluator(input);
        }


        public static TInput Default<TInput>(this TInput input, TInput resultOnNullInChain)
        { // simular as Return, with default x=>x implementation of evaluator {
           if(input==null)return resultOnNullInChain;
           return input;
        }


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

        /// <summary>
        ///  broke maybe chain if predicate return null, othewise pass input
        /// </summary>
        
        public static TInput If<TInput>(this TInput input,Predicate<TInput> evaluator)
        {
            if (input == null) return default(TInput);
            return evaluator(input) ? input : default(TInput);
        }

        /// <summary>
        ///  do not changed context, do action if input is not null
        /// </summary>
        
        public static TInput Do<TInput>(this TInput input,Action<TInput> action) 
        {
            if (input == null) return default(TInput);
            action(input);
            return input;
        }
        
    }
}
