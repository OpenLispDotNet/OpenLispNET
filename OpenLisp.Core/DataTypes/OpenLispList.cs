using System.Collections.Generic;
using System.Linq;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.StaticClasses;

namespace OpenLisp.Core.DataTypes
{
    public class OpenLispList : OpenLispVal
    {
        public string Start = "(";

        public string End = ")";

        private List<OpenLispVal> _value;

        public List<OpenLispVal> Value => _value;

        public int Size => _value.Count;

        public override bool ListQ()
        {
            return true;
        }

        public OpenLispList()
        {
            _value = new List<OpenLispVal>();
        }

        public OpenLispList(List<OpenLispVal> value)
        {
            _value = value;
        }

        public OpenLispList(params OpenLispVal[] values)
        {
            _value = new List<OpenLispVal>();

            Conj(values);
        }

        public OpenLispList Conj(params OpenLispVal[] values)
        {
            //foreach (var v in values)
            //{
            //    _value.Add(v);
            //}

            _value.AddRange(values.ToList());

            return this;
        }

        public override string ToString()
        {
            return Start + Printer.Join(_value, " ", true) + End;
        }

        public override string ToString(bool printReadably)
        {
            return Start + Printer.Join(_value, " ", printReadably) + End;
        }

        public OpenLispVal Nth(int index)
        {
            return _value.Count > index ? _value[index] : StaticOpenLispTypes.Nil;
        }

        public OpenLispVal this[int index] => _value.Count > index ? _value[index] : StaticOpenLispTypes.Nil;

        public OpenLispList Rest()
        {
            return Size > 0 ? new OpenLispList(_value.GetRange(1, _value.Count - 1)) : new OpenLispList();
        }

        public virtual OpenLispList Slice(int start)
        {
            return new OpenLispList(_value.GetRange(start, _value.Count - 1));
        }

        public virtual OpenLispList Slice(int start, int end)
        {
            return new OpenLispList(_value.GetRange(start, end - start));
        }
    }
}