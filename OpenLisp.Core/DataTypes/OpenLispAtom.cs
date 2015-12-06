using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes.Errors.Throwable;

namespace OpenLisp.Core.DataTypes
{
    public class OpenLispAtom : OpenLispVal
    {
        private OpenLispVal _value;

        public OpenLispVal Value
        {
            get
            {
                if (_value == null) throw new OpenLispException("Value is null.");
                return _value;
            }
            //private set { _value = value; }
            set { _value = value; }
        }

        public OpenLispAtom(OpenLispVal value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return "(atom " + StaticClasses.Printer.PrStr(Value, true) + ")";
        }

        public override string ToString(bool printReadably)
        {
            return "(atom " + StaticClasses.Printer.PrStr(Value, printReadably) + ")";
        }
    }
}