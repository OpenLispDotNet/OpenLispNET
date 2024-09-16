using OpenLisp.Core.Kernel.OS.System.Graphics.UI.GUI;
using OpenLisp.Core.Kernel.OS.System.Processing.Applications.Terminal;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Interpreter.Commands.c_Console
{
    class CommandClear : ICommand
    {
        private TerminalApp _terminal;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public CommandClear(string[] commandvalues, Application terminal) : base(commandvalues)
        {
            Description = "to clear the console";
            _terminal = terminal as TerminalApp;
        }

        /// <summary>
        /// CommandClear
        /// </summary>
        public override ReturnInfo Execute()
        {
            if (_terminal != null)
            {
                _terminal.Console.ClearText();
            }
            else
            {
                Cosmos.System.Power.Shutdown();
            }

            return new ReturnInfo(this, ReturnCode.OK);
        }
    }
}
