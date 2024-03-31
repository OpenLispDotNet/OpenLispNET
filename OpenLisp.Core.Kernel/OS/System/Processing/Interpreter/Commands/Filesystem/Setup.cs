﻿using System;
using System.Collections.Generic;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Interpreter.Commands.Filesystem
{
    class CommandSetup : ICommand
    {
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public CommandSetup(string[] commandvalues) : base(commandvalues)
        {
            Description = "to install AuraOS on a disk.";
        }

        public override ReturnInfo Execute(List<string> arguments)
        {
            if (arguments.Count < 3)
            {
                return new ReturnInfo(this, ReturnCode.ERROR_ARG);
            }

            try
            {
                string username = arguments[0];
                string password = arguments[1];
                string hostname = arguments[2];

                Setup setup = new();
                setup.InitSetup(username, password, hostname, "en-US");
            }
            catch (Exception ex)
            {
                return new ReturnInfo(this, ReturnCode.ERROR, ex.ToString());
            }

            return new ReturnInfo(this, ReturnCode.OK);
        }

        /// <summary>
        /// Print /help information
        /// </summary>
        public override void PrintHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine(" - setup {username} {password} {computername}");
        }
    }
}
