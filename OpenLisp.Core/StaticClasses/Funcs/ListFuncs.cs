using System.Collections.Generic;
using System.Linq;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    public class ListFuncs
    {
        public static OpenLispFunc Apply = new OpenLispFunc(x =>
        {
            var f = (OpenLispFunc)x[0];
            var dataList = new List<OpenLispVal>();

            dataList.AddRange(x.Slice(1, x.Size - 1).Value);
            dataList.AddRange(((OpenLispList)x[x.Size - 1]).Value);

            return f.Apply(new OpenLispList(dataList));
        });

        public static OpenLispFunc Map = new OpenLispFunc(x =>
        {
            OpenLispFunc f = (OpenLispFunc)x[0];
            var sourceList = ((OpenLispList)x[1]).Value;
            var newList = sourceList.Select(t => f.Apply(new OpenLispList(t))).ToList();

            return new OpenLispList(newList);
        });
    }
}