using System;
using Sys = Cosmos.System;

namespace OpenLisp.OS
{
    public class Kernel : Sys.Kernel
    {

        protected override void BeforeRun()
        {
            Console.WriteLine("OpenLisp.OS v0.0.1-alpha has started.");

            Sys.FileSystem.CosmosVFS fs = new Cosmos.System.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            
            var available_space = fs.GetAvailableFreeSpace(@"0:\");
            Console.WriteLine("Available Free Space: " + (available_space / 1024 / 1024) + "MB");
            
            var fs_type = fs.GetFileSystemType(@"0:\");
            Console.WriteLine("File System Type: " + fs_type);

            Console.WriteLine("                         _      _                       _   \n" +
                              "                        | |    (_)                     | |  \n" +
                              "   ___  _ __   ___ _ __ | |     _ ___ _ __   _ __   ___| |_ \n" +
                              "  / _ \\| '_ \\ / _ \\ '_ \\| |    | / __| '_ \\ | '_ \\ / _ \\ __|\n" +
                              " | (_) | |_) |  __/ | | | |____| \\__ \\ |_) || | | |  __/ |_ \n" +
                              "  \\___/| .__/ \\___|_| |_|______|_|___/ .__(_)_| |_|\\___|\\__|\n" +
                              "       | |                           | |                    \n" +
                              "       |_|   The Wizard & The Wyrd   |_|                    \n");
            Console.WriteLine("OpenLisp.NET Machine (c) 2015 The Wizard & The Wyrd, LLC");
            Console.WriteLine("Starting the OpenLisp.NET REPL...");
        }

        protected override void Run()
        {

            Console.Write(">>> ");
            var input = Console.ReadLine();
            Console.Write("Text typed: ");
            Console.WriteLine(input);
            //Core.StaticClasses.Repl.ReplMain(new[] { "" });
        }
    }
}
