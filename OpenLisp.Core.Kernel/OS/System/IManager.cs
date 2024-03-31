using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLisp.Core.Kernel.OS.System
{
    public interface IManager
    {
        public void Initialize();
        public string GetName();
    }
}
