using System.Collections.Generic;
using OpenLisp.Core.AbstractClasses;

namespace OpenLisp.Core.DataTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class OpenLispVector : OpenLispList
    {
        /// <summary>
        /// 
        /// </summary>
        public OpenLispVector() 
            : base()
        {
            Start = "[";

            End = "]";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        public OpenLispVector(List<OpenLispVal> val)
            : base(val)
        {
            Start = "[";

            End = "]";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool ListQ()
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public override OpenLispList Slice(int start, int end)
        {
            var value = this.Value;

            return new OpenLispVector(value.GetRange(start, value.Count - start));
        }
    }
}