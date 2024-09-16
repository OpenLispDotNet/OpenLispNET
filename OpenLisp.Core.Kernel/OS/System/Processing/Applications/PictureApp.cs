using Cosmos.System.Graphics;
using OpenLisp.Core.Kernel.OS.System.Graphics.UI.GUI;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Applications
{
    public class PictureApp : Application
    {
        public static string ApplicationName = "Picture";

        private Bitmap _image;

        public PictureApp(string name, Bitmap bitmap, int width, int height, int x = 0, int y = 0) : base(name, width, height, x, y)
        {
            ApplicationName = name;
            _image = bitmap;
        }

        public override void Draw()
        {
            base.Draw();

            DrawImage(_image, 0, 0);
        }
    }
}
