using System.Collections.Generic;
using System.Linq;
using OpenLisp.Core.AbstractClasses;

namespace OpenLisp.Core.DataTypes
{
    public class OpenLispList : OpenLispVal
    {
        public string Start = "(";

        public string End = ")";

        private List<OpenLispVal> _value;

        public List<OpenLispVal> Value => _value;

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
            return Start + printer.
        }
    }
}