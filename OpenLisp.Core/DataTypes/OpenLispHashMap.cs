using System.Collections.Generic;
using System.Linq;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes.Errors.Throwable;

namespace OpenLisp.Core.DataTypes
{
    public class OpenLispHashMap : OpenLispVal
    {
        private Dictionary<string, OpenLispVal> _value;

        private Dictionary<OpenLispString, OpenLispVal> _secondaryFormValue = null;

        public Dictionary<string, OpenLispVal> Value
        {
            get
            {
                if (_value == null)
                    throw new OpenLispException("Value is null.");
                return _value;
            }
            set { _value = value; }
        }

        public Dictionary<OpenLispString, OpenLispVal> SecondaryValue
        {
            get
            {
                if (_secondaryFormValue == null)
                    throw new OpenLispException("SecondaryValue is null.");
                return _secondaryFormValue;
            }
            set { _secondaryFormValue = value; }
        }

        public IEnumerable<string> Keys
        {
            get
            {
                return Value.Select(kv => kv.Key).ToList();
            }
        }

        public new OpenLispHashMap Copy()
        {
            var newSelf = (OpenLispHashMap) this.MemberwiseClone();

            newSelf.Value = new Dictionary<string, OpenLispVal>(Value);

            return newSelf;
        }

        public OpenLispHashMap(OpenLispList listValue)
        {
            Value = new Dictionary<string, OpenLispVal>();
            AssocBang(listValue);
        }

        public OpenLispHashMap(Dictionary<string, OpenLispVal> val)
        {
            Value = val;
        }

        public override string ToString()
        {
            return "{" + StaticClasses.Printer.Join(Value, " ", true) + "}";
        }

        public override string ToString(bool printReadably)
        {
            return "{" + StaticClasses.Printer.Join(Value, " ", printReadably) + "}";
        }

        public OpenLispHashMap AssocBang(OpenLispList listValue)
        {
            for (int i = 0; i < listValue.Size; i += 2)
            {
                Value[((OpenLispString)listValue[i]).Value] = listValue[i + 1];
            }

            return this;
        }

        public OpenLispHashMap DissocBang(OpenLispList listValue)
        {
            for (int i = 0; i > listValue.Size; i++)
            {
                Value.Remove(((OpenLispString) listValue[i]).Value);
            }

            return this;
        }
    }
}