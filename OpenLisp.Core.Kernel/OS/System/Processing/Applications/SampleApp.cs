using OpenLisp.Core.Kernel.OS.System.Graphics.UI.GUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenLisp.Core.Kernel.OS.System.Graphics.UI.GUI.Components;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Applications
{
    public class SampleApp : Application
    {
        public static string ApplicationName = "SampleApp";

        private Component component;
        private DirectBitmap _image;
        private DirectBitmap _image2;

        public SampleApp(int width, int height, int x = 0, int y = 0) : base(ApplicationName, width, height, x, y)
        {
            ForceDirty = true;

            component = new Component(50, 50, 400, 400);
            AddChild(component);

            _image = new DirectBitmap(300, 300);
            _image.Clear(Color.FromArgb(0xFF, 0xFF, 0x00, 0x00).ToArgb());
            _image2 = new DirectBitmap(300, 300);
            _image2.Clear(Color.FromArgb(0x45, 0x00, 0x00, 0xFF).ToArgb());
        }

        public override void Draw()
        {
            base.Draw();

            component.Clear(Color.Green);
            component.Draw();
            component.DrawImage(_image.Bitmap, 0, 0);

            component.DrawImage(_image2.Bitmap, 100, 100);
            component.DrawInParent();
        }
    }
}
