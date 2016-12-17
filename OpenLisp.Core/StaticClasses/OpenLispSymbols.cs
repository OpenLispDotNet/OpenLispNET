using System.Security.Policy;

/*
 * OpenLISP can have new OpenLispSymbols by adding
 * more public static readonly string members to this
 * class.
 * 
 * In your project, be sure to use the same namespace
 * and semantics for your additions to OpenLispSymbols. 
 */ 
//namespace OpenLisp.Core.StaticClasses
//{
//    public static partial class OpenLispSymbols
//    {
//        public static readonly string EqualB             = @"=";
//        public static readonly string Throw              = @"throw";
//        public static readonly string NilB               = @"nil?";
//        public static readonly string TrueB              = @"true?";
//        public static readonly string FalseB             = @"false?";
//        public static readonly string Symbol             = @"symbol";
//        public static readonly string SymbolB            = @"symbol?";
//        public static readonly string Keyword            = @"keyword";
//        public static readonly string KeywordB           = @"keyword?";
//        public static readonly string PrStr              = @"pr-str";
//        public static readonly string Str                = @"str";
//        public static readonly string Prn                = @"prn";
//        public static readonly string PrintLn            = @"println";
//        public static readonly string ReadLine           = @"readline";
//        public static readonly string ReadString         = @"read-string";
//        public static readonly string Slurp              = @"slurp";
//        public static readonly string LessThan           = @"<";
//        public static readonly string LessThanOrEqual    = @"<=";
//        public static readonly string GreaterThan        = @">";
//        public static readonly string GreaterThanOrEqual = @">=";
//        public static readonly string Plus               = @"+";
//        public static readonly string Minus              = @"-";
//        public static readonly string Multiply           = @"*";
//        public static readonly string Divide             = @"/";
//        public static readonly string TimeMs             = @"time-ms";
//        public static readonly string List               = @"list";
//        public static readonly string ListB              = @"list?";
//        public static readonly string Vector             = @"vector";
//        public static readonly string VectorB            = @"vector?";
//        public static readonly string HashMap            = @"hash-map";
//        public static readonly string MapB               = @"map?";
//        public static readonly string ContainsB          = @"contains?";
//        public static readonly string Assoc              = @"assoc";
//        public static readonly string Dissoc             = @"dissoc";
//        public static readonly string Get                = @"get";
//        public static readonly string Keys               = @"keys";
//        public static readonly string Vals               = @"vals";
//        public static readonly string SequentialB        = @"sequential?";
//        public static readonly string Cons               = @"cons";
//        public static readonly string Concat             = @"concat";
//        public static readonly string Nth                = @"nth";
//        public static readonly string First              = @"first";
//        public static readonly string Rest               = @"rest";
//        public static readonly string EmptyB             = @"empty?";
//        public static readonly string Count              = @"count";
//        public static readonly string Conj               = @"conj";
//        public static readonly string Apply              = @"apply";
//        public static readonly string Map                = @"map";
//        public static readonly string WithMeta           = @"with-meta";
//        public static readonly string Meta               = @"meta";
//        public static readonly string Atom               = @"atom";
//        public static readonly string AtomB              = @"atom?";
//        public static readonly string Deref              = @"deref";
//        public static readonly string ResetBang          = @"reset!";
//        public static readonly string SwapBang           = @"swap!";
//    }
//}