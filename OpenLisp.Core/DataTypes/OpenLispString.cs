using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes.Errors.Throwable;

namespace OpenLisp.Core.DataTypes
{
    public class OpenLispString : OpenLispVal
    {
        private string _value;

        public string Value
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_value)) throw new OpenLispException("Value is null, empty, or white-space.");
                return _value;
            }
            set { _value = value; }
        }

        public OpenLispString(string value)
        {
            Value = value;
        }

        public new OpenLispString Copy()
        {
            return this;
        }

        public override string ToString() => "\"" + Value + "\"";

        public override string ToString(bool printReadably)
        {
            if (Value.Length > 0 && Value[0] == '\u029e')
            {   
                return ":" + Value.Substring(1);
            }
            if (printReadably)
            {
                return "\"" + Value.Replace("\\", "\\\\")
                    .Replace("\"", "\\\"")
                    .Replace("\n", "\\n") + "\"";
            }
            return Value;
        }
    }
}