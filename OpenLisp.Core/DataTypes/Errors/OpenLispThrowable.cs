using System;

namespace OpenLisp.Core.DataTypes.Errors
{
    public class OpenLispThrowable : Exception
    {
        public OpenLispThrowable() : base() { }

        public OpenLispThrowable(string msg) : base(msg) { }
    }
}