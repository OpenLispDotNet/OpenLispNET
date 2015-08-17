using OpenLisp.Core.AbstractClasses;

namespace OpenLisp.Core.DataTypes
{
    public class OpenLispSymbol : OpenLispVal
    {
        private string _value;

        public string Value => _value;

        public OpenLispSymbol(string value)
        {
            _value = value;
        }

        public OpenLispSymbol(OpenLispString value)
        {
            _value = value.Value;
        }

        public new OpenLispSymbol Copy()
        {
            return this;
        }

        public override string ToString()
        {
            return _value;
        }

        public override string ToString(bool printReadably)
        {
            return _value;
        }
    }
}