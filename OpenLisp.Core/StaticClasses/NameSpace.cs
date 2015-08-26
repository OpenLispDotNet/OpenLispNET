using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;
using OpenLisp.Core.StaticClasses.Funcs;

namespace OpenLisp.Core.StaticClasses
{
    public static class NameSpace
    {
        public static IDictionary<string, OpenLispVal> Ns = new Dictionary<string, OpenLispVal>
        {
            {"=", new OpenLispFunc(x => StaticOpenLispTypes.Equals)},
        };
    }
}