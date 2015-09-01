using OpenLisp.Core.DataTypes;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    public class ScalarFuncs
    {
        public static OpenLispFunc NilQ = new OpenLispFunc(x => x[0] 
            == StaticOpenLispTypes.Nil 
            ? StaticOpenLispTypes.True 
            : StaticOpenLispTypes.False);

        public static OpenLispFunc TrueQ = new OpenLispFunc(x => x[0]
            == StaticOpenLispTypes.True
            ? StaticOpenLispTypes.True
            : StaticOpenLispTypes.False);

        public static OpenLispFunc FalseQ = new OpenLispFunc(x => x[0]
            == StaticOpenLispTypes.False
            ? StaticOpenLispTypes.True
            : StaticOpenLispTypes.False);

        public static OpenLispFunc SymbolQ = new OpenLispFunc(x => x[0]
            is OpenLispSymbol
            ? StaticOpenLispTypes.True
            : StaticOpenLispTypes.False);

        public static OpenLispFunc Symbol = new OpenLispFunc(x => 
            new OpenLispSymbol((OpenLispString)x[0]));

        public static OpenLispFunc Keyword = new OpenLispFunc(x =>
        {
            var t = x[0] as OpenLispString;
            return t != null &&
                   t.Value[0] == '\u029e'
                ? new OpenLispString(x[0].ToString())
                : new OpenLispString("\u029e" + ((OpenLispString) x[0]).Value);
        });

        public static OpenLispFunc KeywordQ = new OpenLispFunc(x =>
        {
            var t = x[0] as OpenLispString;
            return t != null &&
                   t.Value[0] == '\u029e'
                ? StaticOpenLispTypes.True
                : StaticOpenLispTypes.False;
        });

    }
}
