using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLisp.Core.Events.Args
{
    public class PrintEventArgs : EventArgs
    {
        private string _p;

        public PrintEventArgs(string p)
        {
            this._p = p;
        }

        public string Line
        {
            get
            {
                return this._p;
            }
        }
    }
}
