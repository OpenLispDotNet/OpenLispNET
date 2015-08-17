using OpenLisp.Core.AbstractClasses;

namespace OpenLisp.Core.DataTypes
{
    public class OpenLispConstant : OpenLispVal
    {
        private readonly string _value;

        public OpenLispConstant(string name)
        {
            _value = name;
        }

        public new OpenLispConstant Copy()
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