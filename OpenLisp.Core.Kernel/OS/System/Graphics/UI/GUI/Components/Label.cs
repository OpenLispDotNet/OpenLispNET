using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLisp.Core.Kernel.OS.System.Graphics.UI.GUI.Components
{
    public class Label : Component
    {
        public Color TextColor;
        public string Text = "";

        public Label(string text, Color color, int x, int y) : base(x, y, text.Length * Kernel.font.Width, Kernel.font.Height)
        {
            TextColor = color;
            Text = text;
        }

        public override void Draw()
        {
            Clear(Color.Transparent);

            if (Text != "")
            {
                DrawString(Text, Kernel.font, TextColor, 0, 0);
            }
        }
    }
}
