using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes.Errors.Throwable;

namespace OpenLisp.Core.DataTypes
{
    public class OpenLispHashMap : OpenLispVal
    {
        private ImmutableDictionary<string, OpenLispVal> _value;

        private ImmutableDictionary<OpenLispString, OpenLispVal> _secondaryFormValue = null;

        public ImmutableDictionary<string, OpenLispVal> Value
        {
            get
            {
                if (_value == null)
                    throw new OpenLispException("Value is null.");
                return _value;
            }
            private set { _value = value; }
        }

        public ImmutableDictionary<OpenLispString, OpenLispVal> SecondaryValue
        {
            get
            {
                if (_secondaryFormValue == null)
                    throw new OpenLispException("SecondaryValue is null.");
                return _secondaryFormValue;
            }
            private set { _secondaryFormValue = value; }
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

            newSelf.Value = new Dictionary<string, OpenLispVal>(Value).ToImmutableDictionary();

            return newSelf;
        }

        public OpenLispHashMap(OpenLispList listValue)
        {
            Value = ImmutableDictionary<string, OpenLispVal>.Empty;
            AssocBang(listValue);
        }

        public OpenLispHashMap(ImmutableDictionary<string, OpenLispVal> val)
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
                //Value[((OpenLispString)listValue[i]).Value] = listValue[i + 1];
                Value.SetItem(((OpenLispString)listValue[i]).Value, listValue[i + 1]);
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