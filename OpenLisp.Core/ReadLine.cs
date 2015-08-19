using System;
using OpenLisp.Terminal;

namespace OpenLisp.Core
{
    public class ReadLine
    {
        public enum ModeEnum { Terminal, Raw }

        public static ModeEnum Mode = ModeEnum.Terminal;

        private static LineEditor _lineEditor = null;

        public static string LineReader(string prompt)
        {
            if (Mode == ModeEnum.Terminal)
            {
                if (_lineEditor == null)
                {
                    _lineEditor = new LineEditor("OpenLisp.NET");
                }

                return _lineEditor.Edit(prompt, "");
            }

            Console.WriteLine(prompt);
            Console.Out.Flush();
            return Console.ReadLine();
        }
    }
}