using System;
using System.Collections.Generic;
using System.IO;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    public class AtomFuncs
    {
        public static OpenLispFunc AtomQ = new OpenLispFunc(x => 
            x[0] is OpenLispAtom 
                ? StaticOpenLispTypes.True 
                : StaticOpenLispTypes.False);

        public static OpenLispFunc Deref = new OpenLispFunc(x => ((OpenLispAtom)x[0]).Value);

        public static OpenLispFunc ResetBang = new OpenLispFunc(x => ((OpenLispAtom)x[0]).Value = x[1]);

        public static OpenLispFunc SwapBang = new OpenLispFunc(x =>
        {
            OpenLispAtom dataAtom = (OpenLispAtom)x[0];
            OpenLispFunc f = (OpenLispFunc)x[1];
            var newList = new List<OpenLispVal> {dataAtom.Value};

            newList.AddRange(x.Slice(2).Value);

            return dataAtom.Value = (f.Apply(new OpenLispList(newList)));
        });
    }
}