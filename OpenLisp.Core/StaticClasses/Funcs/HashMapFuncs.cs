using System.Collections.Generic;
using System.Linq;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    /// <summary>
    /// Funcs used by <see cref="OpenLispHashMap"/>.
    /// </summary>
    public class HashMapFuncs
    {
        /// <summary>
        /// Is this a hash map?
        /// </summary>
        public static OpenLispFunc HashMapQ = new OpenLispFunc(x =>
            x[0].GetType() == typeof (OpenLispHashMap) ? StaticOpenLispTypes.True : StaticOpenLispTypes.False);

        /// <summary>
        /// Does this contain collection contain the first parameter?
        /// </summary>
        public static OpenLispFunc ContainsQ = new OpenLispFunc(x =>
        {
            OpenLispString key = new OpenLispString(((OpenLispString)x[1]).Value);
            IDictionary<string, OpenLispVal> dict = (((OpenLispHashMap)x[0]).Value);

            //OpenLispConstant result = dict.Keys.Contains(key.ToString()) ? StaticOpenLispTypes.True : StaticOpenLispTypes.False;
            OpenLispConstant result = dict.ContainsKey(key.ToString()) ? StaticOpenLispTypes.True : StaticOpenLispTypes.False;

            return result;
        });

        /// <summary>
        /// Associate a hash key with a value.
        /// </summary>
        public static OpenLispFunc Assoc = new OpenLispFunc(x =>
        {
            var newHm = ((OpenLispHashMap)x[0]).Copy();

            return newHm.AssocBang(x.Slice(1));
        });

        /// <summary>
        /// Dissassociate a hash key and a value.
        /// </summary>
        public static OpenLispFunc Dissoc = new OpenLispFunc(x =>
        {
            var newHm = ((OpenLispHashMap)x[0]).Copy();

            return newHm.DissocBang(x.Slice(1));
        });

        /// <summary>
        /// Get the first parameter from an <see cref="OpenLispHashMap"/>.
        /// </summary>
        public static OpenLispFunc Get = new OpenLispFunc(x =>
        {
            string key = ((OpenLispString) x[1]).Value;

            if (x[0] == StaticOpenLispTypes.Nil) return StaticOpenLispTypes.Nil;

            var dict = ((OpenLispHashMap)x[0]).Value;
            return dict.ContainsKey(key) ? dict[key] : StaticOpenLispTypes.Nil;
        });

        /// <summary>
        /// Gets the keys of an <see cref="OpenLispHashMap"/>.
        /// </summary>
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

        /// <summary>
        /// Gets the values of an <see cref="OpenLispHashMap"/>.
        /// </summary>
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
        /// <summary>
        /// Does this <see cref="OpenLispHashMap"/> intance contain an instance of <see cref="T:T1"/>
        /// </summary>
        public static OpenLispFunc ContainsQ = new OpenLispFunc(x =>
        {
            Dictionary<string, OpenLispVal> dict = (((OpenLispHashMap)x[0]).Value);

            //T1 key = default(T1);
            //T2 value = default(T2);
            //IDictionary<T1, T2 > genericDict = new Dictionary<T1, T2>();

            //ImmutableList<string> dictKeys = dict.Select(a => a.Key).ToImmutableList();
            //ImmutableList<OpenLispVal> dictValues = dict.Select(b => b.Value).ToImmutableList();

            return dict.Select(z => z.Key is T1).ToList().Any()
                ? StaticOpenLispTypes.True
                : (dict.Any(kv => kv.Value is T2) ? StaticOpenLispTypes.True : StaticOpenLispTypes.False);
        });
    }
}