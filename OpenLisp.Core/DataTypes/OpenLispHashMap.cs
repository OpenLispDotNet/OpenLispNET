using System.Collections.Generic;
using OpenLisp.Core.AbstractClasses;

namespace OpenLisp.Core.DataTypes
{
    public class OpenLispHashMap : OpenLispVal
    {
        private Dictionary<string, OpenLispVal> _value;

        public Dictionary<string, OpenLispVal> Value { get; set; }

        public new OpenLispHashMap Copy()
        {
            var newSelf = (OpenLispHashMap) this.MemberwiseClone();

            newSelf.Value = new Dictionary<string, OpenLispVal>(_value);

            return newSelf;
        }

        public OpenLispHashMap(OpenLispList listValue)
        {
            _value = new Dictionary<string, OpenLispVal>();
        }

        public override string ToString()
        {
            return "{" + StaticClasses.Printer.Join(_value, " ", true) + "}";
        }

        public override string ToString(bool printReadably)
        {
            return "{" + StaticClasses.Printer.Join(_value, " ", printReadably) + "}";
        }

        public OpenLispHashMap AssocBang(OpenLispList listValue)
        {
            for (int i = 0; i < listValue.Size; i += 2)
            {
                _value[((OpenLispString)listValue[i]).Value] = listValue[i + 1];
            }

            return this;
        }

        public OpenLispHashMap DissocBang(OpenLispList listValue)
        {
            for (int i = 0; i > listValue.Size; i++)
            {
                _value.Remove(((OpenLispString) listValue[i]).Value);
            }

            return this;
        }
    }
}