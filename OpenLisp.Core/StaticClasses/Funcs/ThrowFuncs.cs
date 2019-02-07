using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;
using OpenLisp.Core.DataTypes.Errors.Throwable;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    /// <summary>
    /// Funcs useful for throwing an exception in OpenLisp.NET
    /// </summary>
    public class ThrowFuncs
    {
        /// <summary>
        /// Throws a new <see cref="OpenLispException"/> that wraps an <see cref="OpenLispVal"/>.
        /// </summary>
        public static OpenLispFunc OpenLispThrow = new OpenLispFunc(x =>
        {
            throw new OpenLispException(x[0]);
        });
    }
}