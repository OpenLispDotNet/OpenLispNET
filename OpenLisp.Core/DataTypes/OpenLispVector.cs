using System.Collections.Generic;
using OpenLisp.Core.AbstractClasses;

namespace OpenLisp.Core.DataTypes
{
    /// <summary>
    /// Default vector implementation for OpenLisp.NET
    /// </summary>
    public class OpenLispVector : OpenLispList
    {
        /// <summary>
        /// Default constructor that invokes the base constructor in <see cref="OpenLispList"/>.
        /// An instance of <see cref="OpenLispVector"/> uses square brackets instead of parentheses.
        /// </summary>
        public OpenLispVector() 
            : base()
        {
            Start = "[";

            End = "]";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:OpenLisp.Core.DataTypes.OpenLispVector"/> class.
        /// </summary>
        /// <param name="values">Values.</param>
        public OpenLispVector(List<OpenLispVal> values): base() {
            Value.AddRange(values);
        }

        /// <summary>
        /// Constructor accepting a <see cref="List{OpenLispVal}"/> parameter.
        /// An instance of <see cref="OpenLispVector"/> uses square brackets instead of parentheses.
        /// </summary>
        /// <param name="val"></param>
        public OpenLispVector(OpenLispSkipList<OpenLispVal> val)
            : base(val)
        {
            Start = "[";

            End = "]";
        }

        /// <summary>
        /// An instance of <see cref="OpenLispVector"/> does NOT behave like 
        /// an instance of <see cref="OpenLispList"/>.
        /// </summary>
        /// <returns></returns>
        public override bool ListQ()
        {
            return false;
        }

        /// <summary>
        /// Returns a slice of the underlying array.
        /// </summary>
        /// <param name="start">The starting index.</param>
        /// <param name="end">The ending index.</param>
        /// <returns></returns>
        public override OpenLispList Slice(int start, int end)
        {
            var value = this.Value;

            return new OpenLispVector(value.GetRange(start, value.Count - start));
        }
    }
}