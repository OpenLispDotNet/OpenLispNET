using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLisp.Core.Interfaces
{
    /// <summary>
    /// Interface to describe an implementation of a monad in OpenLisp.NET
    /// </summary>
    /// <typeparam name="T">The Type whose behavior you want to bind in your monad.</typeparam>
    public interface IOpenLispMonad<T>
    {
        /// <summary>
        /// Bind to <see cref="IOpenLispMonad{TO}"/> using a <see cref="Func{T, TResult}"/>
        /// </summary>
        /// <typeparam name="TO">The Type bound to in your monad.</typeparam>
        /// <param name="func">The binding function of your monad.</param>
        /// <returns></returns>
        IOpenLispMonad<TO> Bind<TO>(Func<T, IOpenLispMonad<TO>> func);
    }
}
