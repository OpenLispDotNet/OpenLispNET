using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;

namespace OpenLisp.Core.DataTypes.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class OpenLispMonadExtensions
    {
        // WARNING: Uncommenting this will cause IntelliSense to crash IntelliSense
        //          (yes, recursively...) with the following versions:
        //
        //          Microsoft Visual Studio Community 2015
        //          Version 14.9.25420.01 Update 3
        //
        //          Microsoft .NET Framework
        //          Version 4.6.01586
        //
        //public static OpenLispMonad<OpenLispVal> NullToMonad<T>(this T value)
        //{
        //    return value != null ? new OpenLispMonad<OpenLispVal>(value as OpenLispVal) : OpenLispMonad<OpenLispVal>.None();
        //}
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static OpenLispMonad<OpenLispVal> NullToMaybe<T>(this T value) where T : OpenLispVal
        {
            return value != null ? new OpenLispMonad<OpenLispVal>(value as T) : OpenLispMonad<OpenLispVal>.Maybe;
        }
    }
}
