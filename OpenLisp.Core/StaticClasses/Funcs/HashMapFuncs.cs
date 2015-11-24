using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    public class HashMapFuncs
    {
        public static OpenLispFunc HashMapQ = new OpenLispFunc(x =>
            x[0].GetType() == typeof (OpenLispHashMap) ? StaticOpenLispTypes.True : StaticOpenLispTypes.False);

        public static OpenLispFunc ContainsQ = new OpenLispFunc(x =>
        {
            OpenLispString key = new OpenLispString(((OpenLispString)x[1]).Value);
            IDictionary<string, OpenLispVal> dict = (((OpenLispHashMap)x[0]).Value);

            //OpenLispConstant result = dict.Keys.Contains(key.ToString()) ? StaticOpenLispTypes.True : StaticOpenLispTypes.False;
            OpenLispConstant result = dict.ContainsKey(key.ToString()) ? StaticOpenLispTypes.True : StaticOpenLispTypes.False;

            return result;
        });

        public static OpenLispFunc Assoc = new OpenLispFunc(x =>
        {
            var newHm = ((OpenLispHashMap)x[0]).Copy();

            return newHm.AssocBang(x.Slice(1));
        });

        public static OpenLispFunc Dissoc = new OpenLispFunc(x =>
        {
            var newHm = ((OpenLispHashMap)x[0]).Copy();

            return newHm.DissocBang(x.Slice(1));
        });

        public static OpenLispFunc Get = new OpenLispFunc(x =>
        {
            string key = ((OpenLispString) x[1]).Value;

            if (x[0] == StaticOpenLispTypes.Nil) return StaticOpenLispTypes.Nil;

            var dict = ((OpenLispHashMap)x[0]).Value;
            return dict.ContainsKey(key) ? dict[key] : StaticOpenLispTypes.Nil;
        });

        public static OpenLispFunc Keys = new OpenLispFunc(x =>
        {
            var dict = ((OpenLispHashMap)x[0]).Value;

            OpenLispList keyList = new OpenLispList();

            foreach (var key in dict.Keys)
            {
                keyList.Conj(new OpenLispString(key));
            }

            return keyList;
        });

        public static OpenLispFunc Values = new OpenLispFunc(x =>
        {
            var dict = ((OpenLispHashMap)x[0]).Value;

            OpenLispList valueList = new OpenLispList();

            foreach (var value in dict.Values)
            {
                valueList.Conj(value);
            }

            return valueList;
        });
    }

    /// <summary>
    /// A generic Contains method for OpenLispHashMap objects.
    /// </summary>
    /// <typeparam name="T1">A Dictionary key type.</typeparam>
    /// <typeparam name="T2">A Dictionary value type.</typeparam>
    public static class HashMapFuncs<T1, T2>
    {
        public static OpenLispFunc ContainsQ = new OpenLispFunc(x =>
        {
            ImmutableDictionary<string, OpenLispVal> dict = (((OpenLispHashMap)x[0]).Value);

            //T1 key = default(T1);
            //T2 value = default(T2);
            //IDictionary<T1, T2 > genericDict = new Dictionary<T1, T2>();

            //ImmutableList<string> dictKeys = dict.Select(a => a.Key).ToImmutableList();
            //ImmutableList<OpenLispVal> dictValues = dict.Select(b => b.Value).ToImmutableList();

            return dict.Select(z => z.Key is T1).ToImmutableList().Any()
                ? StaticOpenLispTypes.True
                : (dict.Any(kv => kv.Value is T2) ? StaticOpenLispTypes.True : StaticOpenLispTypes.False);
        });
    }
}