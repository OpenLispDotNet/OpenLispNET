using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes.Errors.Throwable;
using OpenLisp.Core.StaticClasses;

namespace OpenLisp.Core.DataTypes
{
    public class OpenLispList : OpenLispVal
    {
        public string Start = "(";

        public string End = ")";

        private ImmutableList<OpenLispVal> _value;

        public ImmutableList<OpenLispVal> Value
        {
            get
            {
                if (_value == null) throw new OpenLispException("Value is null.");
                return _value;
            }
            set { _value = value; }
        }

        public int Size => Value.Count;

        public override bool ListQ()
        {
            return true;
        }

        public OpenLispList()
        {
            Value = ImmutableList<OpenLispVal>.Empty;
        }

        public OpenLispList(ImmutableList<OpenLispVal> value)
        {
            Value = value;
        }

        public OpenLispList(params OpenLispVal[] values)
        {
            Value = ImmutableList<OpenLispVal>.Empty;

            Conj(values);
        }

        public OpenLispList Conj(params OpenLispVal[] values)
        {
            //foreach (var v in values)
            //{
            //    _value.Add(v);
            //}

            Value.AddRange(values.ToImmutableList());

            return this;
        }

        public override string ToString()
        {
            return Start + Printer.Join(Value, " ", true) + End;
        }

        public override string ToString(bool printReadably)
        {
            return Start + Printer.Join(Value, " ", printReadably) + End;
        }

        public OpenLispVal Nth(int index)
        {
            return Value.Count > index ? Value[index] : StaticOpenLispTypes.Nil;
        }

        public OpenLispVal this[int index] => Value.Count > index ? Value[index] : StaticOpenLispTypes.Nil;

        public OpenLispList Rest()
        {
            return Size > 0 ? new OpenLispList(Value.GetRange(1, Value.Count - 1)) : new OpenLispList();
        }

        public virtual OpenLispList Slice(int start)
        {
            return new OpenLispList(Value.GetRange(start, Value.Count - 1));
        }

        public virtual OpenLispList Slice(int start, int end)
        {
            return new OpenLispList(Value.GetRange(start, end - start));
        }
    }
}