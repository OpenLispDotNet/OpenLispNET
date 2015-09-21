using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace OpenLisp.Machine.Kernel
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.WriteLine("                         _      _                       _   \n" +
                              "                        | |    (_)                     | |  \n" +
                              "   ___  _ __   ___ _ __ | |     _ ___ _ __   _ __   ___| |_ \n" +
                              "  / _ \\| '_ \\ / _ \\ '_ \\| |    | / __| '_ \\ | '_ \\ / _ \\ __|\n" +
                              " | (_) | |_) |  __/ | | | |____| \\__ \\ |_) || | | |  __/ |_ \n" +
                              "  \\___/| .__/ \\___|_| |_|______|_|___/ .__(_)_| |_|\\___|\\__|\n" +
                              "       | |                           | |                    \n" +
                              "       |_|   The Wizard & The Wyrd   |_|                    \n");
            Console.WriteLine("OpenLisp.NET Machine © 2015 The Wizard & The Wyrd, LLC");
            Console.WriteLine("Starting the OpenLisp.NET REPL...");
        }

        protected override void Run()
        {
            //Console.Write("Input: ");
            //var input = Console.ReadLine();
            //Console.Write("Text typed: ");
            //Console.WriteLine(input);

            // This is complaining about missing a plug detailed in the README.txt
            Core.StaticClasses.Repl.ReplMain(new [] {""});
        }
    }
}
