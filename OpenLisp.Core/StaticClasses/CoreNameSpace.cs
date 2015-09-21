using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DeepEqual.Syntax;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;
using OpenLisp.Core.StaticClasses.Funcs;

namespace OpenLisp.Core.StaticClasses
{
    public static partial class CoreNameSpace
    {
        public static IDictionary<string, OpenLispVal> Ns = new Dictionary<string, OpenLispVal>
        {
#if !NOTYPEEQUALITY
            {"=",           new OpenLispFunc(x => 
                                StaticOpenLispTypes.OpenLispEqualQ(x[0], x[1]) 
                                    ? StaticOpenLispTypes.True 
                                    : StaticOpenLispTypes.False)},
#elif NOTYPEEQUALITY
            {"=",           new OpenLispFunc(x => 
                                (x[0].ToString() == x[1].ToString())
                                    ? StaticOpenLispTypes.True
                                    : StaticOpenLispTypes.False)},
#endif

            {"throw",       ThrowFuncs.OpenLispThrow},

            {"nil?",        ScalarFuncs.NilQ},
            {"true?",       ScalarFuncs.TrueQ},
            {"false?",      ScalarFuncs.FalseQ},
            {"symbol",      ScalarFuncs.Symbol},
            {"symbol?",     ScalarFuncs.SymbolQ},
            {"keyword",     ScalarFuncs.Keyword},
            {"keyword?",    ScalarFuncs.KeywordQ},

            {"pr-str",      StringFuncs.PrStr},
            {"str",         StringFuncs.Str},
            {"prn",         StringFuncs.Prn},
            {"println",     StringFuncs.PrintLn},
            {"readline",    StringFuncs.OpenLispReadLine},
            {"read-string", StringFuncs.ReadString},
            {"slurp",       StringFuncs.Slurp},

            {"<",           new OpenLispFunc(x => 
                                (OpenLispInt)x[0] < (OpenLispInt)x[1])},
            {"<=",          new OpenLispFunc(x =>
                                (OpenLispInt)x[0] <= (OpenLispInt)x[1])},
            {">",           new OpenLispFunc(x =>
                                (OpenLispInt)x[0] > (OpenLispInt)x[1])},
            {">=",          new OpenLispFunc(x =>
                                (OpenLispInt)x[0] >= (OpenLispInt)x[1])},
            {"+",           new OpenLispFunc(x =>
                                (OpenLispInt)x[0] + (OpenLispInt)x[1])},
            {"-",           new OpenLispFunc(x =>
                                (OpenLispInt)x[0] - (OpenLispInt)x[1])},
            {"*",           new OpenLispFunc(x =>
                                (OpenLispInt)x[0] * (OpenLispInt)x[1])},
            {"/",           new OpenLispFunc(x =>
                                (OpenLispInt)x[0] / (OpenLispInt)x[1])},

            {"time-ms",     NumberFuncs.TimeMs},

            {"list",        new OpenLispFunc(x => new OpenLispList(x.Value))},
            {"list?",       CollectionFuncs.ListQ},

            {"vector",      new OpenLispFunc(x => new OpenLispVector(x.Value))},
            {"vector?",     CollectionFuncs.VectorQ},

            {"hash-map",    new OpenLispFunc(x => new OpenLispHashMap(x))},
            {"map?",        HashMapFuncs.HashMapQ},
            {"contains?",   HashMapFuncs.ContainsQ},
            {"assoc",       HashMapFuncs.Assoc},
            {"dissoc",      HashMapFuncs.Dissoc},
            {"get",         HashMapFuncs.Get},
            {"keys",        HashMapFuncs.Keys},
            {"vals",        HashMapFuncs.Values},

            {"sequential?", SequenceFuncs.SequentialQ},
            {"cons",        SequenceFuncs.Cons},
            {"concat",      SequenceFuncs.Concat},
            {"nth",         SequenceFuncs.Nth},
            {"first",       SequenceFuncs.First},
            {"rest",        SequenceFuncs.Rest},
            {"empty?",      SequenceFuncs.EmptyQ},
            {"count",       SequenceFuncs.Count},
            {"conj",        SequenceFuncs.Conj},

            {"apply",       ListFuncs.Apply},
            {"map",         ListFuncs.Map},

            {"with-meta",   MetadataFuncs.WithMeta},
            {"meta",        MetadataFuncs.Meta},

            {"atom",        new OpenLispFunc(x => new OpenLispAtom(x[0]))},
            {"atom?",       AtomFuncs.AtomQ},
            {"deref",       AtomFuncs.Deref},
            {"reset!",      AtomFuncs.ResetBang},
            {"swap!",       AtomFuncs.SwapBang}
        };
    }
}