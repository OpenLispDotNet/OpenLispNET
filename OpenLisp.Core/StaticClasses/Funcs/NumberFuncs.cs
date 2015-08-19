using System;
using OpenLisp.Core.DataTypes;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    public class NumberFuncs
    {
         public static OpenLispFunc TimeMs = new OpenLispFunc(x =>
            new OpenLispInt(DateTime.Now.Ticks / 10000L));
    }
}