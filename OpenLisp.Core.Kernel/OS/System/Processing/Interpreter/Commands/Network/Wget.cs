﻿using System;
using System.Collections.Generic;
using System.IO;
using OpenLisp.Core.Kernel.OS.System.Network;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Interpreter.Commands.Network
{
    class CommandWget : ICommand
    {
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public CommandWget(string[] commandvalues) : base(commandvalues, CommandType.Network)
        {
            Description = "to download a file through HTTP.";
        }

        /// <summary>
        /// CommandDns
        /// </summary>
        /// <param name="arguments">Arguments</param>
        public override ReturnInfo Execute()
        {
            PrintHelp();
            return new ReturnInfo(this, ReturnCode.OK);
        }

        /// <summary>
        /// CommandDns
        /// </summary>
        /// <param name="arguments">Arguments</param>
        public override ReturnInfo Execute(List<string> arguments)
        {
            string url = arguments[0];

            if (url.StartsWith("https://"))
            {
                return new ReturnInfo(this, ReturnCode.ERROR_ARG, "HTTPS currently not supported, please use http://");
            }

            try
            {
                string file = Http.DownloadFile(url);

                File.WriteAllText(Kernel.CurrentDirectory + "file.html", file);

                Console.WriteLine(url + " saved to file.html");
            }
            catch (Exception ex)
            {
                return new ReturnInfo(this, ReturnCode.ERROR_ARG, "Exception: " + ex);
            }

            return new ReturnInfo(this, ReturnCode.OK);
        }

        /// <summary>
        /// Print /help information
        /// </summary>
        public override void PrintHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine(" - wget {url}");
        }
    }
}
