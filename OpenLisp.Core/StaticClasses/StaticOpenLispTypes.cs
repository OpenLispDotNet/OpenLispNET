using System;
using System.Collections;
using DeepEqual.Syntax;
using OpenLisp.Core.DataTypes;

namespace OpenLisp.Core.StaticClasses
{
    public static class StaticOpenLispTypes
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

        public static OpenLispConstant Nil = new OpenLispConstant("nil");

        public static OpenLispConstant True = new OpenLispConstant("true");

        public static OpenLispConstant False = new OpenLispConstant("false");
    }
}