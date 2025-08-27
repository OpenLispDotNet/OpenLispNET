using System.Collections.Generic;

namespace OpenLisp.Core.DataTypes
{
    /// <summary>
    /// Matrix composed of a collection of equal-length OpenLispVector types.
    /// </summary>
    public class OpenLispMatrix : OpenLispVector
    {
        /// <summary>
        /// A Matrix is a collection of vectors where each vector is of n dimensions.
        /// Matrices contain x amount of vectors encoded as row matrix values:
        ///
        ///     | [0, 1, 2, 3] [3, 2, 1, 0 ] |
        ///
        /// We want to minimize excessive syntax to expression our notations.
        /// </summary>
        public new List<OpenLispVector> Value { get; set; }

        public OpenLispMatrix() 
            : base()
        {
            Start = "|";

            End = "|";
        }

        public OpenLispMatrix(List<OpenLispVector> val) 
            : base()
        {
            Value = val;
        }
    }
}
