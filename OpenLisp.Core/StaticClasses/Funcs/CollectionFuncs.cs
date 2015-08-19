using OpenLisp.Core.DataTypes;
using static OpenLisp.Core.StaticClasses.StaticOpenLispTypes;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    public class CollectionFuncs
    {
        public static OpenLispFunc ListQ = new OpenLispFunc(x =>
            x[0].GetType() == typeof(OpenLispList)
                ? True
                : False);

        public static OpenLispFunc VectorQ = new OpenLispFunc(x =>
            x[0].GetType() == typeof(OpenLispVector)
                ? True
                : False);
    }

    public class CollectionFuncs<T>
    { 
        public static OpenLispFunc GenericQ = new OpenLispFunc(x =>
            x[0].GetType() == typeof(T)
                ? True
                : False);

    }
}