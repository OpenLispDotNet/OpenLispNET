using OpenLisp.Core.Kernel.OS.System.Graphics.UI.GUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Processes
{
    public class Explorer : Process
    {
        public static bool ShowStartMenu
        {
            get
            {
                return _showStartMenu;
            }
            set
            {
                _showStartMenu = value;
                StartMenu.Visible = _showStartMenu;

                if (StartMenu.Visible)
                {
                    WindowManager.BringToFront(StartMenu);
                }
            }
        }

        public static Taskbar Taskbar;
        public static StartMenu StartMenu;
        public static Desktop Desktop;
        public static LoginScreen Login;
        public static WindowManager WindowManager = new WindowManager();
        public static DirectBitmap Screen;

        private static bool _showStartMenu = false;

        public Explorer() : base("Explorer", ProcessType.KernelComponent)
        {
            Screen = new((int)Kernel.ScreenWidth, (int)Kernel.ScreenHeight);
            WindowManager.Initialize();
            WindowManager.SetScreen(Screen);

            CustomConsole.WriteLineInfo("Starting desktop...");
            Desktop = new Desktop(0, 0, (int)Kernel.ScreenWidth, (int)Kernel.ScreenHeight);

            CustomConsole.WriteLineInfo("Starting setup...");
            Login = new LoginScreen(0, 0, (int)Kernel.ScreenWidth, (int)Kernel.ScreenHeight);

            CustomConsole.WriteLineInfo("Starting task bar...");
            Taskbar = new Taskbar();
            Taskbar.UpdateApplicationButtons();
            Taskbar.Visible = false;

            CustomConsole.WriteLineInfo("Starting start menu...");
            if (Kernel.Installed)
            {
                int menuWidth = 168;
                int menuHeight = 35 * 11;
                int menuX = 0;
                int menuY = (int)(Kernel.ScreenHeight - menuHeight - Taskbar.taskbarHeight);
                StartMenu = new StartMenu(menuX, menuY, menuWidth, menuHeight);
            }
            else
            {
                int menuWidth = 168;
                int menuHeight = 35 * 10;
                int menuX = 0;
                int menuY = (int)(Kernel.ScreenHeight - menuHeight - Taskbar.taskbarHeight);
                StartMenu = new StartMenu(menuX, menuY, menuWidth, menuHeight);
            }

            if (Kernel.Installed)
            {
                Settings config = new Settings(@"0:\System\settings.ini");
                string value = config.GetValue("autologin");
                string computerName = config.GetValue("hostname");
                Kernel.ComputerName = computerName;

                if (value == "true")
                {
                    Taskbar.Visible = true;
                    Login.Hide();
                }
                else
                {
                    Kernel.LoggedIn = false;
                }
            }
            else
            {
                Taskbar.Visible = true;
                Login.Hide();
            }
        }

        public override void Initialize()
        {
            CustomConsole.WriteLineInfo("Starting Explorer process...");

            base.Initialize();

            Kernel.ProcessManager.Register(this);
            Kernel.ProcessManager.Start(this);
        }

        public override void Update()
        {
            if (Kernel.LoggedIn)
            {
                StartMenu.Update();
                Taskbar.Update();

                WindowManager.DrawWindows();

                if (Kernel.MouseManager.IsLeftButtonDown)
                {
                    if (!StartMenu.IsInside((int)MouseManager.X, (int)MouseManager.Y) && !Taskbar.StartButton.IsInside((int)MouseManager.X, (int)MouseManager.Y))
                    {
                        ShowStartMenu = false;
                    }
                }
            }
            else
            {
                Login.Update();

                WindowManager.DrawWindows();
            }
        }
    }
}
