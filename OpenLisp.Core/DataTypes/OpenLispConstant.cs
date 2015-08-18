using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes.Errors.Throwable;

namespace OpenLisp.Core.DataTypes
{
    public class OpenLispConstant : OpenLispVal
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

        public OpenLispConstant(string name)
        {
            Value = name;
        }

        public new OpenLispConstant Copy()
        {
            return this;
        }

        public override string ToString()
        {
            return Value;
        }

        public override string ToString(bool printReadably)
        {
            return Value;
        }
    }
}