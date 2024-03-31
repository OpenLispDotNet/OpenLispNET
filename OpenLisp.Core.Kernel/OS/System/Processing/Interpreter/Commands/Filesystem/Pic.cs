﻿using System;
using System.Collections.Generic;
using System.IO;
using Cosmos.System.Graphics;
using OpenLisp.Core.Kernel.OS.System.Processing.Processes;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Interpreter.Commands.Filesystem
{
    class CommandPicture : ICommand
    {
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public CommandPicture(string[] commandvalues) : base(commandvalues, CommandType.Filesystem)
        {
            Description = "to display a bitmap in a new window";
        }

        public override ReturnInfo Execute(List<string> arguments)
        {
            if (arguments.Count < 1)
            {
                return new ReturnInfo(this, ReturnCode.ERROR_ARG);
            }

            try
            {
                string path = arguments[0];
                string name = Path.GetFileName(path);
                byte[] bytes = File.ReadAllBytes(Kernel.CurrentDirectory + path);
                Bitmap bitmap = new Bitmap(bytes);
                int width = name.Length * 8 + 50;

                if (width < bitmap.Width)
                {
                    width = (int)bitmap.Width + 6;
                }

                var app = new PictureApp(name, bitmap, width, (int)bitmap.Height + 26, 40, 40);
                app.MarkFocused();
                app.Initialize();
                app.Visible = true;

                Explorer.WindowManager.Applications.Add(app);
                Kernel.ProcessManager.Start(app);

                Explorer.Taskbar.UpdateApplicationButtons();

                return new ReturnInfo(this, ReturnCode.OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return new ReturnInfo(this, ReturnCode.ERROR);
            }
        }


        public override void PrintHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine(" - pic {source_file}");
        }
    }
}
