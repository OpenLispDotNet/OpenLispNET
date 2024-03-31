using Cosmos.Debug.Kernel;
using IL2CPU.API.Attribs;
using OpenLisp.Core.Kernel.OS.System;

namespace OpenLisp.Core.Kernel.Plugs
{
    [Plug(Target = typeof(Debugger))]
    public class DebuggerImpl
    {
        public static void DoSend(string aText)
        {
            Logs.DoKernelLog(aText);
        }
    }
}
