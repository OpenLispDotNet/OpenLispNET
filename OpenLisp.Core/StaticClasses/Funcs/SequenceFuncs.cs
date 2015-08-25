using System.Collections.Generic;
using System.Linq;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;
using OpenLisp.Core.DataTypes.Errors.Throwable;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    public class SequenceFuncs : OpenLispVal
    {
        public static OpenLispFunc SequentialQ = new OpenLispFunc(x => 
            x[0] is OpenLispList 
                ? StaticOpenLispTypes.True 
                : StaticOpenLispTypes.False);

        public static OpenLispFunc Cons = new OpenLispFunc(x =>
        {
            var listData = new List<OpenLispVal>();

            listData.Add(x[0]);
            listData.AddRange(((OpenLispList) x[1]).Value);

            return (OpenLispVal) new OpenLispList(listData);
        });

        public static OpenLispFunc Concat = new OpenLispFunc(x =>
        {
            if (x.Size == 0) return new OpenLispList();

            var listData = new List<OpenLispVal>();
            listData.AddRange(((OpenLispList) x[0]).Value);

            for (int i = 1; i < x.Size; i++)
            {
                listData.AddRange(((OpenLispList) x[i]).Value);
            }

            return new OpenLispList(listData);
        });

        public static OpenLispFunc Nth = new OpenLispFunc(x =>
        {
            
        });
    }
}