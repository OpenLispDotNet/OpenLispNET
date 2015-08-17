namespace OpenLisp.Core.DataTypes.Errors
{
    public class OpenLispError : OpenLispThrowable
    {
         public OpenLispError(string msg) : base(msg) { }
    }
}