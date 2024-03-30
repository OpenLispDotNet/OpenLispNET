using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLisp.Core.Events.Args
{
    /// <summary>
    /// Event arguments used when printing expressions in OpenLisp.NET REPLs, etc.
    /// </summary>
    public class PrintEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor accepting a <see cref="string"/> parameter.
        /// </summary>
        /// <param name="p"></param>
        public PrintEventArgs(string p)
        {
            this.Line = p;
        }

        /// <summary>
        /// Get the line that was printed.
        /// </summary>
        public string Line { get; }
    }
}
