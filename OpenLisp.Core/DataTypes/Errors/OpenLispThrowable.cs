using System;

namespace OpenLisp.Core.DataTypes.Errors
{
    /// <summary>
    /// Basic OpenLisp.NET excpetion type that other exceptions MUST inherit from.
    /// </summary>
    public class OpenLispThrowable : Exception
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        protected OpenLispThrowable() : base() { }

        /// <summary>
        /// Constructor accepting a <see cref="string"/> parameter.
        /// </summary>
        /// <param name="msg"></param>
        protected OpenLispThrowable(string msg) : base(msg) { }
    }
}