using OpenLisp.Core.AbstractClasses;

namespace OpenLisp.Core.DataTypes
{
    public class OpenLispAtom : OpenLispVal
    {
        private OpenLispVal _value;

        public OpenLispVal Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public OpenLispAtom(OpenLispVal value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return "(atom " + StaticClasses.Printer.PrStr(_value, true) + ")";
        }

        public override string ToString(bool printReadably)
        {
            return "(atom " + StaticClasses.Printer.PrStr(_value, printReadably) + ")";
        }
    }
}