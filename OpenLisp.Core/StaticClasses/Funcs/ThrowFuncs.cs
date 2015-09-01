using OpenLisp.Core.DataTypes;
using OpenLisp.Core.DataTypes.Errors.Throwable;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    public class ThrowFuncs
    {
        public static OpenLispFunc OpenLispThrow = new OpenLispFunc(x =>
        {
            throw new OpenLispException(x[0]);
        });
    }
}