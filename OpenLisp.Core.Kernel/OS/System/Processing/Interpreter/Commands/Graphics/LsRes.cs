using System;
using System.Text;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Interpreter.Commands.Graphics
{
    class CommandLsRes : ICommand
    {
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public CommandLsRes(string[] commandvalues) : base(commandvalues)
        {
            Description = "to list available screen resolutions";
        }

        /// <summary>
        /// CommandDns
        /// </summary>
        /// <param name="arguments">Arguments</param>
        public override ReturnInfo Execute()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Available modes:");

            foreach (var mode in Kernel.Canvas.AvailableModes)
            {
                sb.AppendLine("- " + mode.ToString());
            }

            Console.WriteLine(sb.ToString());

            return new ReturnInfo(this, ReturnCode.OK);
        }
    }
}
