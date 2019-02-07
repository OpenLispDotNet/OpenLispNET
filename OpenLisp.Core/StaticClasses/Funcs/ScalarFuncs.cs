using OpenLisp.Core.DataTypes;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    /// <summary>
    /// Funcs used by scalar values in OpenLisp.NET
    /// </summary>
    public class ScalarFuncs
    {
        /// <summary>
        /// Is it <see cref="StaticOpenLispTypes.Nil"/>?
        /// </summary>
        public static OpenLispFunc NilQ = new OpenLispFunc(x => x[0] 
            == StaticOpenLispTypes.Nil 
            ? StaticOpenLispTypes.True 
            : StaticOpenLispTypes.False);

        /// <summary>
        /// Is it <see cref="StaticOpenLispTypes.True"/>?
        /// </summary>
        public static OpenLispFunc TrueQ = new OpenLispFunc(x => x[0]
            == StaticOpenLispTypes.True
            ? StaticOpenLispTypes.True
            : StaticOpenLispTypes.False);

        /// <summary>
        /// Is it <see cref="StaticOpenLispTypes.False"/>?
        /// </summary>
        public static OpenLispFunc FalseQ = new OpenLispFunc(x => x[0]
            == StaticOpenLispTypes.False
            ? StaticOpenLispTypes.True
            : StaticOpenLispTypes.False);

        /// <summary>
        /// Is this an <see cref="OpenLispSymbol"/>?
        /// </summary>
        public static OpenLispFunc SymbolQ = new OpenLispFunc(x => x[0]
            is OpenLispSymbol
            ? StaticOpenLispTypes.True
            : StaticOpenLispTypes.False);

        /// <summary>
        /// Returns a new <see cref="OpenLispSymbol"/> built on an <see cref="OpenLispString"/>,
        /// and wrapped in the context of an <see cref="OpenLispFunc"/>.
        /// </summary>
        public static OpenLispFunc Symbol = new OpenLispFunc(x => 
            new OpenLispSymbol((OpenLispString)x[0]));

        /// <summary>
        /// Creates a new keyword wrapped in the context of an <see cref="OpenLispFunc"/>.
        /// </summary>
        public static OpenLispFunc Keyword = new OpenLispFunc(x =>
        {
            var t = x[0] as OpenLispString;
            return t != null &&
                   t.Value[0] == '\u029e'
                ? new OpenLispString(x[0].ToString())
                : new OpenLispString("\u029e" + ((OpenLispString) x[0]).Value);
        });

        /// <summary>
        /// Is this an OpenLisp.NET keyword?
        /// </summary>
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
