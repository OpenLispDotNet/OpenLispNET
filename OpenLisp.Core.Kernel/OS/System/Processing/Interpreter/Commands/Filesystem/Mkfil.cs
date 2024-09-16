using System;
using System.Collections.Generic;
using System.IO;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Interpreter.Commands.Filesystem
{
    class CommandMkfil : ICommand
    {
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public CommandMkfil(string[] commandvalues) : base(commandvalues, CommandType.Filesystem)
        {
            Description = "to create a file";
        }

        /// <summary>
        /// CommandMkfil
        /// </summary>
        public override ReturnInfo Execute()
        {
            PrintHelp();

            return new ReturnInfo(this, ReturnCode.OK);
        }

        /// <summary>
        /// CommandMkfil
        /// </summary>
        public override ReturnInfo Execute(List<string> arguments)
        {
            string file = arguments[0];

            if (!File.Exists(Kernel.CurrentDirectory + file))
            {
                File.Create(Kernel.CurrentDirectory + file);
            }
            else
            {
                Console.WriteLine(file + " already exists!");
            }
            return new ReturnInfo(this, ReturnCode.OK);
        }

        /// <summary>
        /// Print /help information
        /// </summary>
        public override void PrintHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine(" - mkfir {file}");
        }
    }
}
