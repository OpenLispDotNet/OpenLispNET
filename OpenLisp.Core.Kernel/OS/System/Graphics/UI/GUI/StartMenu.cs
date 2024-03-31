﻿using Cosmos.System.Graphics;
using OpenLisp.Core.Kernel.OS.System.Graphics.UI.GUI.Components;
using System;
using System.Collections.Generic;
using Cosmos.HAL;
using OpenLisp.Core.Kernel.OS.System.Processing.Processes;

namespace OpenLisp.Core.Kernel.OS.System.Graphics.UI.GUI
{
    public class StartMenu : Window
    {
        private List<Button> buttons;
        private Button Shutdown;
        private Button Reboot;
        private Button Logout;

        public StartMenu(int x, int y, int width, int height) : base(x, y, width, height)
        {
            Frame = Kernel.ThemeManager.GetFrame("window.borderless");

            buttons = new List<Button>();

            // Shutdown
            Shutdown = new Button(Kernel.ResourceManager.GetIcon("24-shutdown.bmp"), "Shut Down.", 3, Height - 35 - 3, Width - 6, 35);
            Shutdown.Click = new Action(() =>
            {
                Power.ACPIShutdown();
            });
            AddChild(Shutdown);

            // Reboot
            Reboot = new Button(Kernel.ResourceManager.GetIcon("24-reboot.bmp"), "Reboot.", 3, Height - 70 - 3, Width - 6, 35);
            Reboot.Click = new Action(() =>
            {
                Power.CPUReboot();
            });
            AddChild(Reboot);

            if (Kernel.Installed)
            {
                // Logout
                Logout = new Button(Kernel.ResourceManager.GetIcon("24-logout.bmp"), "Log Out.", 3, Height - 105 - 3, Width - 6, 35);
                Logout.Click = new Action(() =>
                {
                    if (Kernel.LoggedIn)
                    {
                        Kernel.LoggedIn = false;
                        Explorer.Login.Show();
                    }
                });
                AddChild(Logout);
            }

            // App buttons
            buttons.Clear();
            int buttonY = 0;
            foreach (var applicationConfig in Kernel.ApplicationManager.ApplicationTemplates)
            {
                Bitmap icon = null;

                if (applicationConfig.Template.Name.StartsWith("Terminal"))
                {
                    icon = Kernel.ResourceManager.GetIcon("24-terminal.bmp");
                }
                else if (applicationConfig.Template.Name.StartsWith("Explorer"))
                {
                    icon = Kernel.ResourceManager.GetIcon("24-explorer.bmp");
                }
                else if (applicationConfig.Template.Name.StartsWith("Settings"))
                {
                    icon = Kernel.ResourceManager.GetIcon("24-settings.bmp");
                }
                else
                {
                    icon = Kernel.ResourceManager.GetIcon("24-program.bmp");
                }

                var button = new Button(icon, applicationConfig.Template.Name, 3, 3 + buttonY + 1, Width - 6, 35);
                button.Click = new Action(() =>
                {
                    Kernel.ApplicationManager.StartApplication(applicationConfig);
                    Explorer.ShowStartMenu = false;
                });
                buttons.Add(button);
                AddChild(button);
                buttonY += 35;
            }

            Visible = false;
        }

        public override void Update()
        {
            foreach (var button in buttons)
            {
                button.Update();
            }

            Shutdown.Update();
            Reboot.Update();
            if (Kernel.Installed)
            {
                Logout.Update();
            }
        }

        public override void Draw()
        {
            base.Draw();

            foreach (var button in buttons)
            {
                button.Draw(this);
            }

            Shutdown.Draw(this);
            Reboot.Draw(this);
            if (Kernel.Installed)
            {
                Logout.Draw(this);
            }
        }
    }
}
