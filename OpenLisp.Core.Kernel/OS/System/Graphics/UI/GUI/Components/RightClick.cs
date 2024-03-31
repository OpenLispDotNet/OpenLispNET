﻿using System.Collections.Generic;

namespace OpenLisp.Core.Kernel.OS.System.Graphics.UI.GUI.Components
{
    public class RightClickEntry : Button
    {
        public const int ConstHeight = 16 + 4;
        RightClick _rightclick;

        public RightClickEntry(string text, int width, RightClick rightclick) : base(text, 0, 0, width, ConstHeight)
        {
            TextAlignStyle = TextAlign.Left;
            _rightclick = rightclick;
            Visible = false;
        }

        public override void HandleLeftClick()
        {
            base.HandleLeftClick();

            _rightclick.Opened = false;
        }
    }

    public class RightClick : Window
    {
        public List<RightClickEntry> Entries;

        private bool _opened = false;
        public bool Opened
        {
            get
            {
                return _opened;
            }
            set
            {
                Visible = value;
                _opened = value;

                foreach (var entry in Entries)
                {
                    entry.Visible = value;
                }
            }
        }

        public RightClick(int x, int y, int width, int height) : base(x, y, width, height)
        {
            Frame = Kernel.ThemeManager.GetFrame("window.borderless");
            Visible = false;
            HasBorders = false;
            Entries = new List<RightClickEntry>();
        }

        public void AddEntry(RightClickEntry entry)
        {
            AddChild(entry);
            Entries.Add(entry);
        }

        public override void Update()
        {
            foreach (var entry in Entries)
            {
                entry.Update();
            }
        }

        public override void Draw()
        {
            base.Draw();

            int currentY = 0;
            foreach (var entry in Entries)
            {
                entry.Y = currentY;
                currentY += entry.Height;

                entry.Draw(this);
            }
        }
    }
}
