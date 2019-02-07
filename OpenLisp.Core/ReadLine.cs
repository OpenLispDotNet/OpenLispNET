using System;
using OpenLisp.Terminal;

namespace OpenLisp.Core
{
    /// <summary>
    /// Read line object; mostly used by the OpenLisp.NET REPL.
    /// </summary>
    public class ReadLine
    {
        /// <summary>
        /// Terminal or Raw mode reading?
        /// 
        /// TODO: put into its own source file to decouple the enum from <see cref="ReadLine"/>.
        /// </summary>
        public enum ModeEnum {
            /// <summary>
            /// Suitable for user-visible shells and REPLs.
            /// </summary>
            Terminal,

            /// <summary>
            /// Suitable for non-interactive CLI tools and REPL background services.
            /// </summary>
            Raw
        }

        /// <summary>
        /// Readline instances default to <see cref="ModeEnum.Terminal"/>.
        /// </summary>
        public static ModeEnum Mode = ModeEnum.Terminal;

        /// <summary>
        /// The <see cref="LineEditor"/> singleton.
        /// </summary>
        private static LineEditor _lineEditor = null;

        /// <summary>
        /// Static method to present a prompt in a REPL.
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
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