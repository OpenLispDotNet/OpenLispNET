using OpenLisp.Core.DataTypes;
using static OpenLisp.Core.StaticClasses.StaticOpenLispTypes;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    /// <summary>
    /// Funcs used by classes that derive from <see cref="OpenLispList"/>.
    /// </summary>
    public class CollectionFuncs
    {
        /// <summary>
        /// Is this an intance of an <see cref="OpenLispList"/>?
        /// </summary>
        public static OpenLispFunc ListQ = new OpenLispFunc(x =>
            x[0].GetType() == typeof(OpenLispList)
                ? True
                : False);

        /// <summary>
        /// Is this an instance of <see cref="OpenLispVector"/>?
        /// </summary>
        public static OpenLispFunc VectorQ = new OpenLispFunc(x =>
            x[0].GetType() == typeof(OpenLispVector)
                ? True
                : False);
    }

    /// <summary>
    /// Generic functions for types inheriting from <see cref="OpenLispList"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CollectionFuncs<T>
    { 
        /// <summary>
        /// Is this a type of T?
        /// </summary>
        public static OpenLispFunc GenericQ = new OpenLispFunc(x =>
            x[0].GetType() == typeof(T)
                ? True
                : False);

    }
}