using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes.Errors.Throwable;
using OpenLisp.Core.DataTypes.Errors;
using OpenLisp.Core.DataTypes;
using OpenLisp.Core.Events.Args;
using OpenLisp.Core.StaticClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sys = Cosmos.System;

namespace OpenLisp.Core.Kernel
{
    public class Kernel : Sys.Kernel
    {

        protected override void BeforeRun()
        {
            //Console.WriteLine("                         _      _                       _   \n" +
            //                  "                        | |    (_)                     | |  \n" +
            //                  "   ___  _ __   ___ _ __ | |     _ ___ _ __   _ __   ___| |_ \n" +
            //                  "  / _ \\| '_ \\ / _ \\ '_ \\| |    | / __| '_ \\ | '_ \\ / _ \\ __|\n" +
            //                  " | (_) | |_) |  __/ | | | |____| \\__ \\ |_) || | | |  __/ |_ \n" +
            //                  "  \\___/| .__/ \\___|_| |_|______|_|___/ .__(_)_| |_|\\___|\\__|\n" +
            //                  "       | |                           | |                    \n" +
            //                  "       |_|   The Wizard & The Wyrd   |_|                    \n");
            //Console.WriteLine("OpenLisp.NET Machine (c) 2015, 2024 The Wizard & The Wyrd, LLC\n");

            string banner = "   ____                   __    _              _   ______________\n" +
                            "  / __ \\____  ___  ____  / /   (_)________    / | / / ____/_  __/\n" +
                            " / / / / __ \\/ _ \\/ __ \\/ /   / / ___/ __ \\  /  |/ / __/   / /   \n" +
                            "/ /_/ / /_/ /  __/ / / / /___/ (__  ) /_/ / / /|  / /___  / /    \n" +
                            "\\____/ .___/\\___/_/ /_/_____/_/____/ .___(_)_/ |_/_____/ /_/     \n" +
                            "    /_/                           /_/                            \n";
            Console.WriteLine(banner);
            Console.WriteLine("OpenLisp.NET");
            Console.WriteLine("v0.0.6-alpha");
            Console.WriteLine("(c) The Wizard & The Wyrd, LLC");
            Console.WriteLine("Starting the OpenLisp.NET REPL...");
        }

        protected override void Run()
        {
            //Console.Write("Input: ");
            //var input = Console.ReadLine();
            //Console.Write("Text typed: ");
            //Console.WriteLine(input);

            //Console.Write("wizard> ");
            //var input = Console.ReadLine();

            Core.StaticClasses.Repl.ReplMain(new[] { "" });
        }
    }
}
