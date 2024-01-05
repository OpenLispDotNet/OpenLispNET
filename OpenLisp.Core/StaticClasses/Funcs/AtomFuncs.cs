using System;
using System.Collections.Generic;
using System.IO;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    /// <summary>
    /// Funcs used by <see cref="OpenLispAtom"/>.
    /// </summary>
    public static class AtomFuncs
    {
        /// <summary>
        /// Is this an atom?
        /// </summary>
        public static readonly OpenLispFunc AtomQ = new OpenLispFunc(x => 
            x[0] is OpenLispAtom 
                ? StaticOpenLispTypes.True 
                : StaticOpenLispTypes.False);

        /// <summary>
        /// Dereference an atom.
        /// </summary>
        public static readonly OpenLispFunc Deref = new OpenLispFunc(x => ((OpenLispAtom)x[0]).Value);

        /// <summary>
        /// reset! an atom.
        /// </summary>
        public static readonly OpenLispFunc ResetBang = new OpenLispFunc(x => ((OpenLispAtom)x[0]).Value = x[1]);

        /// <summary>
        /// swap! an atom.
        /// </summary>
        public static readonly OpenLispFunc SwapBang = new OpenLispFunc(x =>
        {
            OpenLispAtom dataAtom = (OpenLispAtom)x[0];
            OpenLispFunc f = (OpenLispFunc)x[1];
            var newList = new List<OpenLispVal> {dataAtom.Value};

            newList.AddRange(x.Slice(2).Value);

            return dataAtom.Value = (f.Apply(new OpenLispList(newList)));
        });
    }
}
