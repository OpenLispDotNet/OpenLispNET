using System.Collections;
using System.Collections.Generic;

namespace OpenLisp.Core.Interfaces
{
    /// <summary>
    /// Generic interface describing a collection of a list of symbols used to store expressions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILispExpression<T>
    {
        /// <summary>
        /// <see cref="IList"/> collection of <see cref="ILispSymbol{T}"/>.
        /// </summary>
        IList<ILispSymbol<T>> OpenLispExpression { get; set; }
    }
}