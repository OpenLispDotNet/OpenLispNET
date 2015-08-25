using System.Collections.Generic;
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
            var listData = new List<OpenLispVal> {x[0]};

            listData.AddRange(((OpenLispList) x[1]).Value);

            return new OpenLispList(listData);
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
            var index = (int)((OpenLispInt)x[1]).Value;

            if (index < ((OpenLispList) x[0]).Size)
            {
                return ((OpenLispList) x[0])[index];
            }

            throw new OpenLispException($"Nth: index: {index} out of range: {((OpenLispList) x[0]).Size}");
        });

        public static OpenLispFunc First = new OpenLispFunc(x => ((OpenLispList)x[0])[0]);

        public static OpenLispFunc Rest = new OpenLispFunc(x => ((OpenLispList)x[0]).Rest());

        public static OpenLispFunc EmptyQ = new OpenLispFunc(x =>
            ((OpenLispList) x[0]).Size == 0
                ? StaticOpenLispTypes.True
                : StaticOpenLispTypes.False);

        public static OpenLispFunc Count = new OpenLispFunc(x => 
            (x[0] == StaticOpenLispTypes.Nil)
                ? new OpenLispInt(0)
                : new OpenLispInt(((OpenLispList)x[0]).Size));

        public static OpenLispFunc Conj = new OpenLispFunc(x =>
        {
            var sourceList = ((OpenLispList)x[0]).Value;
            var newList = new List<OpenLispVal>();

            newList.AddRange(sourceList);

            if (x[0] is OpenLispVector)
            {
                for (int i = 1; i < x.Size; i++)
                {
                    newList.Add(x[i]);
                }

                return new OpenLispVector(newList);
            }

            for (int i = 1; i < x.Size; i++)
            {
                newList.Insert(0, x[i]);
            }

            return new OpenLispList(newList);
        });
    }
}