using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;

namespace OpenLisp.Core.StaticClasses
{
    public static class Printer
    {
        public static string Join(IList<OpenLispVal> values, string delim, bool printReadably)
        {
            return String.Join(delim, values.Select(v => v.ToString(printReadably)).ToArray());
        }

        public static string Join(IDictionary<string, OpenLispVal> values, string delim, bool printReadably)
        {
            List<string> strings = new List<string>();

            foreach (KeyValuePair<string, OpenLispVal> v in values)
            {
                if (v.Key.Length > 0 && v.Key[0] == '\u029e')
                {
                    strings.Add(":" + v.Key.Substring(1));
                }
                else if (printReadably)
                {
                    strings.Add("\"" + v.Key + "\"");
                }
                else
                {
                    strings.Add(v.Key);
                }

                strings.Add(v.Value.ToString(printReadably));
            }

            return String.Join(delim, strings.ToArray());
        }

        public static string PrStr(OpenLispVal value, bool printReadably)
        {
            return value.ToString(printReadably);
        }

        public static string PrStrArgs(OpenLispList args, String separator, bool printReadably)
        {
            return Join(args.Value, separator, printReadably);
        }

        public static string EscapeString(string str)
        {
            return Regex.Escape(str);
        }
    }
}