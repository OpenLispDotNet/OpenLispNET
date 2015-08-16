using System;

namespace OpenLisp.Core.Interfaces
{
    public interface ILispSymbol<T>
    {
        string OpenLispSymbol { get; set; }

        Func<T> OpenLispMethodFunc { get; set; }
    }
}