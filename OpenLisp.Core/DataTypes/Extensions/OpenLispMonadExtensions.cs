using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;

namespace OpenLisp.Core.DataTypes.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class OpenLispMonadExtensions
    {
        /// <summary>
        /// Encapsulate a null value into the OpenLispMonad.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static OpenLispMonad<OpenLispVal> NullToMonad<T>(this T value) where T : OpenLispVal
        {
            return value != null ? new OpenLispMonad<OpenLispVal>(value as T) : OpenLispMonad<OpenLispVal>.Monad;
        }

        /// <summary>
        /// Lift a null value into the Maybe monad.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static OpenLispMonad<OpenLispVal> NullToMaybe<T>(this T value) where T : OpenLispVal
        {
            return value != null ? new OpenLispMonad<OpenLispVal>(value as T) : OpenLispMonad<OpenLispVal>.Maybe;
        }
    }
}
