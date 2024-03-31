using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Interpreter.Commands
{
    class CommandAction : ICommand
    {

        private Action _action;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public CommandAction(string[] commandvalues, Action action) : base(commandvalues)
        {
            _action = action;
        }

        /// <summary>
        /// RebootCommand
        /// </summary>
        public override ReturnInfo Execute()
        {
            _action();
            return new ReturnInfo(this, ReturnCode.OK);
        }
    }
}
