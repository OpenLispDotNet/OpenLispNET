using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLisp.Core.DataTypes
{
    /// <summary>
    /// Currently, we can either wrap a <see cref="OpenLispMonad"/> in
    /// other compiled types or with another <see cref="OpenLispVal"/>
    /// inside the OpenLisp.Net runtime <see cref="Env"/>
    /// </summary>
    public class OpenLispMonad : IOpenLispMonad<OpenLispVal>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public IOpenLispMonad<U> Bind<U>(Func<IOpenLispMonad<OpenLispVal>, IOpenLispMonad<U>> func)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="openLispFunc"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public IOpenLispMonad<OpenLispVal> TypeConstructor(OpenLispFunc openLispFunc, Env environment)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public IOpenLispMonad<OpenLispVal> UnitFunction(OpenLispVal dataType)
        {
            throw new NotImplementedException();
        }
    }
}
