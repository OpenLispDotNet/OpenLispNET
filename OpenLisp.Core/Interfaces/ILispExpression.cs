using System.Collections;
using System.Collections.Generic;

namespace OpenLisp.Core.Interfaces
{
    public interface ILispExpression<T>
    {
        IList<ILispSymbol<T>> LispExpression { get; set; }
    }
}