using System;

namespace OpenLisp.Core.Interfaces
{
    /// <summary>
    /// Interface to describe an instance of an OpenLisp.NET symbol.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILispSymbol<T>
    {
        /// <summary>
        /// String representation of an OpenLisp.NET symbol.
        /// </summary>
        string OpenLispSymbol { get; set; }

        /// <summary>
        /// <see cref="Func{T}"/> of an OpenLisp.NET function returning void.
        /// </summary>
        Func<T> OpenLispMethodFunc { get; set; }
    }
}