using System.Collections;
using System.Collections.Generic;
using OpenLisp.Core.DataTypes;
using System.Linq;
using OpenLisp.Core.AbstractClasses;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    public class HashMapFuncs
    {
        public static OpenLispFunc HashMapQ = new OpenLispFunc(x =>
            x[0].GetType() == typeof (OpenLispHashMap) ? StaticOpenLispTypes.True : StaticOpenLispTypes.False);

        public static OpenLispFunc ContainsQ = new OpenLispFunc(x =>
        {
            OpenLispString key = new OpenLispString(((OpenLispString)x[1]).Value);
            IDictionary<string, OpenLispVal> dict = new Dictionary<string, OpenLispVal>();
            dict = (((OpenLispHashMap)x[0]).Value);

            //OpenLispConstant result = dict.Contains<OpenLispString>(key) ? StaticOpenLispTypes.True : StaticOpenLispTypes.False;
            OpenLispConstant result = dict.Contains(key.ToString()) ? StaticOpenLispTypes.True : StaticOpenLispTypes.False;
            //var result = dict.Contains<string, T>(key.Value) ? StaticOpenLispTypes.True : StaticOpenLispTypes.False;         

            return StaticOpenLispTypes.True;
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
            Dictionary<string, OpenLispVal> dict = (((OpenLispHashMap)x[0]).Value);

            T1 key = default(T1);
            T2 value = default(T2);
            IDictionary<T1, T2 > genericDict = new Dictionary<T1, T2>();

            List<string> dictKeys = dict.Select(a => a.Key).ToList();
            List<OpenLispVal> dictValues = dict.Select(b => b.Value).ToList();

            return dict.Select(z => z.Key is T1).ToList().Any()
                ? StaticOpenLispTypes.True
                : (dict.Any(kv => kv.Value is T2) ? StaticOpenLispTypes.True : StaticOpenLispTypes.False);
        });
    }
}