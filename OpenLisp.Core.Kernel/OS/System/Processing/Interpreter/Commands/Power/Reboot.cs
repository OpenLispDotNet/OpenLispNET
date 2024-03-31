using System;
using Sys = Cosmos.System;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Interpreter.Commands.Power
{
    class CommandReboot : ICommand
    {
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public CommandReboot(string[] commandvalues) : base(commandvalues)
        {
            Description = "to do a CPU reboot";
        }

        /// <summary>
        /// RebootCommand
        /// </summary>
        public override ReturnInfo Execute()
        {
            Console.WriteLine("Restarting...");
            Sys.Power.Reboot();
            return new ReturnInfo(this, ReturnCode.OK);
        }
    }
}
