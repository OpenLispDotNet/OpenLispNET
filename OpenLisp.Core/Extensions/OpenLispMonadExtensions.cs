using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;
using OpenLisp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLisp.Core.Extensions
{
    /// <summary>
    /// OpenLispMonad Extensions
    /// </summary>
    public static class OpenLispMonadExtensions
    {
        /// <summary>
        /// Fluent monadic return function.
        /// 
        /// Example:
        /// 
        ///     immutableValue0
        ///         .Return()
        ///         .Bind(result0 => transformedIntoResult1(result0))
        ///         .Bind(result1 => transformedIntoResult2(result1))
        ///         .Bind(result2 => transformedIntoResult3(result2))
        ///         .Bind(result3 => transformedIntoResult4(result3))
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static OpenLispMonad<T> Return<T>(this T instance, Env env) where T : OpenLispVal => new OpenLispMonad<T>(instance, env);
    }
}
