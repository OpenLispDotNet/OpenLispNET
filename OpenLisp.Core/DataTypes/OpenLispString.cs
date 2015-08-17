using OpenLisp.Core.AbstractClasses;

namespace OpenLisp.Core.DataTypes
{
    public class OpenLispString : OpenLispVal
    {
        private string _value;

        public string Value => _value;

        public OpenLispString(string value)
        {
            _value = value;
        }

        public new OpenLispString Copy()
        {
            return this;
        }

        public override string ToString() => "\"" + _value + "\"";

        public override string ToString(bool printReadably)
        {
            if (_value.Length > 0 && _value[0] == '\u029e')
            {   
                return ":" + _value.Substring(1);
            }
            if (printReadably)
            {
                return "\"" + _value.Replace("\\", "\\\\")
                    .Replace("\"", "\\\"")
                    .Replace("\n", "\\n") + "\"";
            }
            return _value;
        }
    }
}