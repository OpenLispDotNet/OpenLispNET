﻿using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using OpenLisp.Core.Kernel.OS.System.Parser;
using OpenLisp.Core.Kernel.OS.System.Users;
using OpenLisp.Core.Kernel.OS.System.Utils;

namespace OpenLisp.Core.Kernel.OS.System.Graphics.UI.GUI.Skin
{
    public class SkinParsing
    {
        private Dictionary<string, Bitmap> _bitmaps = new Dictionary<string, Bitmap>();
        private Dictionary<string, Frame> _frames = new Dictionary<string, Frame>();
        private string _skinName;

        public void loadSkin(string skinXmlContent)
        {
            NanoXMLDocument xml = new NanoXMLDocument(skinXmlContent);
            NanoXMLNode skin = xml.RootNode;

            CustomConsole.WriteLineOK("xml skin loaded");

            foreach (NanoXMLNode node in skin.SubNodes)
            {
                if (node.Name.Equals("resources"))
                {
                    loadResources(node);
                }
            }

            CustomConsole.WriteLineOK("resources loaded");

            foreach (NanoXMLNode node in skin.SubNodes)
            {
                if (node.Name.Equals("frames"))
                {
                    loadFrames(node);
                }
            }

            CustomConsole.WriteLineOK("frames loaded");
        }

        private void loadResources(NanoXMLNode resourcesNode)
        {
            foreach (NanoXMLNode node in resourcesNode.SubNodes)
            {
                if (node.Name.Equals("bitmap"))
                {
                    string bitmapName = node.GetAttribute("name").Value;
                    _skinName = node.GetAttribute("contentPath").Value;

                    string bmpPath;

                    if (Kernel.Installed)
                    {
                        Settings config = new Settings(@"0:\System\settings.ini");
                        bmpPath = config.GetValue("themeBmpPath");

                        if (!File.Exists(bmpPath))
                        {
                            bmpPath = Files.IsoVolume + "UI\\Themes\\" + _skinName + ".bmp";
                        }
                    }
                    else
                    {
                        bmpPath = Files.IsoVolume + "UI\\Themes\\" + _skinName + ".bmp";
                    }

                    try
                    {
                        Bitmap bitmap = new Bitmap(File.ReadAllBytes(bmpPath));
                        Kernel.ThemeManager.BmpPath = bmpPath;
                        _bitmaps.Add(bitmapName, bitmap);
                        CustomConsole.WriteLineOK("Bitmap '" + bitmapName + "' added successfully!");
                    }
                    catch (Exception e)
                    {
                        CustomConsole.WriteLineError("Failed to load bitmap '" + bitmapName + "': " + e.Message);
                    }
                }
            }
        }

        private void loadFrames(NanoXMLNode framesNode)
        {
            foreach (NanoXMLNode node in framesNode.SubNodes)
            {
                if (node.Name.Equals("frame"))
                {
                    string name = node.GetAttribute("name").Value;

                    if (name.StartsWith("window") || name.StartsWith("button") ||
                        name.StartsWith("slider") || name.StartsWith("rail") ||
                        name.StartsWith("cursor") || name.StartsWith("check") || name.StartsWith("input"))
                    {
                        Frame.Region[] regions = RegionListBuilder.Build(node, _bitmaps);
                        Frame.Text[] texts = null;

                        _frames.Add(name, new Frame(regions, texts));

                        CustomConsole.WriteLineOK(name + " added successfully!");
                    }
                }
            }
        }

        public Frame GetFrame(string name)
        {
            return _frames[name];
        }

        public string GetSkinName()
        {
            return _skinName;
        }
    }
}
