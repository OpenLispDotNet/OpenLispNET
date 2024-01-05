using System;
using OpenLisp.Core.DataTypes;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    /// <summary>
    /// Number funcs used by OpenLisp.NET
    /// </summary>
    public static class NumberFuncs
    {
        /// <summary>
        /// Returns the current time in milliseconds.
        /// </summary>
         public static readonly OpenLispFunc TimeMs = new OpenLispFunc(x =>
            new OpenLispInt(DateTime.Now.Ticks / 10000L));
    }
}
