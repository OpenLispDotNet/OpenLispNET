using System;
using System.Collections;
using DeepEqual.Syntax;

namespace OpenLisp.Core.StaticClasses
{
    public static class OpenLispTypes
    {
        public static bool OpenLispEqualB(object a, object b)
        {
            Type typeA = a.GetType();
            Type typeB = b.GetType();

            if (!(typeA == typeB) && !(a.IsDeepEqual(b)))
            {
                return false;
            }
            return true;
        }
    }
}