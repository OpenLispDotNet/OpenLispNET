using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLisp.Core.Interfaces
{
    public interface IOpenLispMonad<T>
    {
        IOpenLispMonad<TO> Bind<TO>(Func<T, IOpenLispMonad<TO>> func);
    }
}
