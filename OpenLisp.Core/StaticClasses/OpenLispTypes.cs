using System;

namespace OpenLisp.Core.StaticClasses
{
    public static class OpenLispTypes
    {
        public static bool OpenLispEqualB(object a, object b)
        {
            Type typeA = a.GetType();
            Type typeB = b.GetType();
        }
    }
}