using System;
using System.IO;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    /// <summary>
    /// Funcs used by <see cref="OpenLispString"/>.
    /// </summary>
    public class StringFuncs
    {
        /// <summary>
        /// Pretty-prints an <see cref="OpenLispString"/> wrapped
        /// in the context of an <see cref="OpenLispFunc"/>.
        /// </summary>
        public static OpenLispFunc PrStr = new OpenLispFunc(x =>
            new OpenLispString(Printer.PrStrArgs(x, " ", true)));

        /// <summary>
        /// Prints an <see cref="OpenLispString"/> wrapped
        /// in the context of an <see cref="OpenLispFunc"/>.
        /// </summary>
        public static OpenLispFunc Str = new OpenLispFunc(x => 
            new OpenLispString(Printer.PrStrArgs(x, " ", false)));

        /// <summary>
        /// Pretty-prints an <see cref="OpenLispString"/> and returns
        /// <see cref="StaticOpenLispTypes.Nil"/> wrapped in the contet of
        /// an <see cref="OpenLispFunc"/>; like returning void in C#, etc.
        /// </summary>
        public static OpenLispFunc Prn = new OpenLispFunc(x =>
        {
            Console.WriteLine(Printer.PrStrArgs(x, " ", true));
            return StaticOpenLispTypes.Nil;
        });

        /// <summary>
        /// Prints an <see cref="OpenLispString"/> and returns
        /// <see cref="StaticOpenLispTypes.Nil"/> wrapped in the contet of
        /// an <see cref="OpenLispFunc"/>; like returning void in C#, etc.
        /// </summary>
        public static OpenLispFunc PrintLn = new OpenLispFunc(x =>
        {
            Console.WriteLine(Printer.PrStrArgs(x, " ", false));
            return StaticOpenLispTypes.Nil;
        });

        /// <summary>
        /// Reads a line as an <see cref="OpenLispVal"/> wrapped
        /// in the context of an <see cref="OpenLispFunc"/>.
        /// </summary>
        public static OpenLispFunc OpenLispReadLine = new OpenLispFunc(x =>
        {
            var line = ReadLine.LineReader(((OpenLispString)x[0]).Value);
            return line == null ? (OpenLispVal) StaticOpenLispTypes.Nil : new OpenLispString(line);
        });

        /// <summary>
        /// Reads an <see cref="OpenLispString"/> wrapped in the context of an <see cref="OpenLispFunc"/>.
        /// </summary>
        public static OpenLispFunc ReadString = new OpenLispFunc(x =>
            Reader.ReadStr(((OpenLispString)x[0]).Value));

        /// <summary>
        /// Slurps a file from disk into a new <see cref="OpenLispString"/> that
        /// can then be evaluated to return an instance of an object inheriting from <see cref="OpenLispVal"/>.
        /// </summary>
        public static OpenLispFunc Slurp = new OpenLispFunc(x =>
            new OpenLispString(File.ReadAllText(((OpenLispString)x[0]).Value)));
    }
}