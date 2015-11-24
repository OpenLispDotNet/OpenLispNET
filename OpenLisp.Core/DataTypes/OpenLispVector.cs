using System.Collections.Generic;
using System.Collections.Immutable;
using OpenLisp.Core.AbstractClasses;

namespace OpenLisp.Core.DataTypes
{
    public class OpenLispVector : OpenLispList
    {
        public OpenLispVector() : base()
        {
            Start = "[";

            End = "]";
        }

        public OpenLispVector(ImmutableList<OpenLispVal> val)
            : base(val)
        {
            Start = "[";

            End = "]";
        }

        public override bool ListQ()
        {
            return false;
        }

        public override OpenLispList Slice(int start, int end)
        {
            var value = this.Value;

            return new OpenLispVector(value.GetRange(start, value.Count - start));
        }
    }
}