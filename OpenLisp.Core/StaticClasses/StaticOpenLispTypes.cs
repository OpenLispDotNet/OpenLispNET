using System;
using DeepEqual.Syntax;
using OpenLisp.Core.AbstractClasses;
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

        public static bool OpenLispEqualQ(OpenLispVal a, OpenLispVal b)
        {
            Type typeA = a.GetType();
            Type typeB = b.GetType();


            if ((typeA != typeB) && (!(a is OpenLispList) || !(b is OpenLispList))) return false;

            if (a is OpenLispInt)
            {
                return ((OpenLispInt) a).Value ==
                       ((OpenLispInt) b).Value;
            }

            if (a is OpenLispSymbol)
            {
                return ((OpenLispSymbol) a).Value ==
                       ((OpenLispSymbol) b).Value;
            }

            if (a is OpenLispString)
            {
                return ((OpenLispString) a).Value ==
                       ((OpenLispString) b).Value;
            }

            if (!(a is OpenLispList)) return a == b;

            if (((OpenLispList) a).Size != ((OpenLispList) b).Size) return false;

            for (int i = 0; i < ((OpenLispList) a).Size; i++)
            {
                if (OpenLispEqualQ(((OpenLispList) a)[i], ((OpenLispList) b)[i]))
                    continue;

                return false;
            }

            return true;
            //return (typeA == typeB) || (a is OpenLispList && b is OpenLispList);
        }

        public static OpenLispConstant Nil = new OpenLispConstant("nil");

        public static OpenLispConstant True = new OpenLispConstant("true");

        public static OpenLispConstant False = new OpenLispConstant("false");
    }
}