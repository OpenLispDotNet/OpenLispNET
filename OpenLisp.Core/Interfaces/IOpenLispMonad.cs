using OpenLisp.Core.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLisp.Core.Interfaces
{
    /// <summary>
    /// Interface to describe an implementation of a monad in OpenLisp.NET
    /// 
    /// Any implementation of this interface should be considered the Monadic type.
    /// Any typeparam passed in as type T should be consider the Monadic Data type.
    /// 
    /// https://en.wikipedia.org/wiki/Monad_(functional_programming)#Formal_definition
    /// </summary>
    /// <typeparam name="T">The Data Type you want to bind in your monad.</typeparam>
    public interface IOpenLispMonad<T>
    {
        /// <summary>
        /// Bind to <see cref="IOpenLispMonad{TO}"/> using a <see cref="Func{T, TResult}"/>
        /// 
        /// Formally: 
        /// 
        ///     (M t) -> (t -> M u) -> (M u)
        /// 
        /// TODO: represent with the ">>=" to correspond to the familiar binding operation of Haskell.
        /// </summary>
        /// <typeparam name="U">The Data Type bound to in your monad.</typeparam>
        /// <param name="func">The binding function of your monad.</param>
        /// <returns></returns>
        IOpenLispMonad<U> Bind<U>(Func<IOpenLispMonad<T>, IOpenLispMonad<U>> func);

        /// <summary>
        /// A type constructor that defines, for every underlying type, how to obtain a corresponding monadic type.
        /// </summary>
        /// <param name="openLispFunc"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        IOpenLispMonad<T> TypeConstructor(OpenLispFunc openLispFunc, Env environment);

        /// <summary>
        /// Injects a value in an underlying type to a value in the 
        /// corresponding monadic type.
        /// 
        /// Formally:
        /// 
        ///     t -> M t
        /// 
        /// </summary>
        /// <returns></returns>
        IOpenLispMonad<T> UnitFunction(T dataType);
    }
}
