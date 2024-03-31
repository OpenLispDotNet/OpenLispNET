using System;
using System.Text;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Interpreter.Commands.Processing
{
    class CommandLsprocess : ICommand
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CommandLsprocess(string[] commandvalues) : base(commandvalues)
        {
            Description = "to list all registered processes";
        }

        /// <summary>
        /// CommandLsprocess
        /// </summary>
        public override ReturnInfo Execute()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("ID      TYPE    STATE    NAME");

            for (int i = 0; i < Kernel.ProcessManager.Processes.Count; i++)
            {
                sb.Append(Kernel.ProcessManager.Processes[i].ID.ToString().PadRight(8, ' '));
                sb.Append(((int)Kernel.ProcessManager.Processes[i].Type).ToString().PadRight(8, ' '));
                sb.Append((Kernel.ProcessManager.Processes[i].Running ? 1 : 0).ToString().PadRight(9, ' '));
                sb.Append(Kernel.ProcessManager.Processes[i].Name.ToString().PadRight(24, ' '));

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());

            return new ReturnInfo(this, ReturnCode.OK);
        }
    }
}
