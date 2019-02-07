namespace OpenLisp.Core.DataTypes.Errors
{
    /// <summary>
    /// Exception when OpenLisp.NET has an error.
    /// </summary>
    public class OpenLispError : OpenLispThrowable
    {
        /// <summary>
        /// Constructor accepting a <see cref="string"/> parameter passed to the base constructor.
        /// </summary>
        /// <param name="msg"></param>
        public OpenLispError(string msg) : base(msg) { }
    }
}