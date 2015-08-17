using System;
using OpenLisp.Core.AbstractClasses;

namespace OpenLisp.Core.DataTypes
{
    public class OpenLispFunc : OpenLispVal
    {
        private Func<OpenLispList, OpenLispVal> _lambda = null;

        private OpenLispVal _ast = null;
    }
}