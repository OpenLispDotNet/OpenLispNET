using System;
using System.Collections.Generic;
using System.IO;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Interpreter.Commands.Filesystem
{
    class CommandRm : ICommand
    {
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public CommandRm(string[] commandvalues) : base(commandvalues, CommandType.Filesystem)
        {
            Description = "to remove a file or directory";
        }

        /// <summary>
        /// CommandRm
        /// </summary>
        public override ReturnInfo Execute(List<string> arguments)
        {
            string path = arguments[0];
            string fullPath = Kernel.CurrentDirectory + path;

            if (System.Filesystem.Entries.ForceRemove(fullPath))
            {
                return new ReturnInfo(this, ReturnCode.OK);
            }

            return new ReturnInfo(this, ReturnCode.ERROR);
        }

        /// <summary>
        /// Print /help information
        /// </summary>
        public override void PrintHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine(" - rm {file/directory}");
        }
    }
}
