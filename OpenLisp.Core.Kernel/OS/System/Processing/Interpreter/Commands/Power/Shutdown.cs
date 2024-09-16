using System;
using Sys = Cosmos.System;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Interpreter.Commands.Power
{
    class CommandShutdown : ICommand
    {
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public CommandShutdown(string[] commandvalues) : base(commandvalues)
        {
            Description = "to do an ACPI shutdown";
        }

        /// <summary>
        /// ShutdownCommand
        /// </summary>
        public override ReturnInfo Execute()
        {
            Console.WriteLine("Shutting Down...");
            Sys.Power.Shutdown();
            return new ReturnInfo(this, ReturnCode.OK);
        }
    }
}
