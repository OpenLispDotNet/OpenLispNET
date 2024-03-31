﻿using System.IO;
using OpenLisp.Core.Kernel.OS.System.Users;
using OpenLisp.Core.Kernel.OS.System.Utils;

namespace OpenLisp.Core.Kernel.OS.System.Graphics.UI.GUI.Skin
{
    /// <summary>
    /// Manages themes for the user interface, loading and applying skins.
    /// </summary>
    public class ThemeManager : IManager
    {
        /// <summary>
        /// The skin parser used to load and interpret theme files.
        /// </summary>
        private SkinParsing _skinParser;

        public string XmlPath;
        public string BmpPath;

        /// <summary>
        /// Loads the default theme and initializes the theme management system.
        /// </summary>
        public void Initialize()
        {
            CustomConsole.WriteLineInfo("Starting theme manager...");

            _skinParser = new SkinParsing();

            if (Kernel.Installed)
            {
                Settings config = new Settings(@"0:\System\settings.ini");
                XmlPath = config.GetValue("themeXmlPath");

                if (!File.Exists(XmlPath))
                {
                    XmlPath = Files.IsoVolume + "UI\\Themes\\Suave.skin.xml";
                }
            }
            else
            {
                XmlPath = Files.IsoVolume + "UI\\Themes\\Suave.skin.xml";
            }

            _skinParser.loadSkin(File.ReadAllText(XmlPath));
        }

        /// <summary>
        /// Retrieves the specified frame by name from the currently loaded theme.
        /// </summary>
        /// <param name="name">The name of the frame to retrieve.</param>
        /// <returns>The requested frame if found; otherwise, null.</returns>
        public Frame GetFrame(string name)
        {
            return _skinParser.GetFrame(name);
        }

        /// <summary>
        /// Gets the name of the currently loaded theme.
        /// </summary>
        /// <returns>The name of the current theme.</returns>
        public string GetThemeName()
        {
            return _skinParser.GetSkinName();
        }

        /// <summary>
        /// Returns the name of the manager.
        /// </summary>
        /// <returns>The name of the manager.</returns>
        public string GetName()
        {
            return "Theme Manager";
        }
    }
}
