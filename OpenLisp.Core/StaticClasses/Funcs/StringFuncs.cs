using System;
using System.IO;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;
using OpenLisp.Terminal;

namespace OpenLisp.Core.StaticClasses.Funcs
{
    public class StringFuncs
    {
        public static OpenLispFunc PrStr = new OpenLispFunc(x =>
            new OpenLispString(Printer.PrStrArgs(x, " ", true)));

        public static OpenLispFunc Str = new OpenLispFunc(x => 
            new OpenLispString(Printer.PrStrArgs(x, " ", false)));

        public static OpenLispFunc Prn = new OpenLispFunc(x =>
        {
            Console.WriteLine(Printer.PrStrArgs(x, " ", true));
            return StaticOpenLispTypes.Nil;
        });

        public static OpenLispFunc PrintLn = new OpenLispFunc(x =>
        {
            Console.WriteLine(Printer.PrStrArgs(x, " ", false));
            return StaticOpenLispTypes.Nil;
        });

        public static OpenLispFunc OpenLispReadLine = new OpenLispFunc(x =>
        {
            var line = ReadLine.LineReader(((OpenLispString)x[0]).Value);
            return line == null ? (OpenLispVal) StaticOpenLispTypes.Nil : new OpenLispString(line);
        });

        static public OpenLispFunc ReadString = new OpenLispFunc(x =>
            Reader.ReadStr(((OpenLispString)x[0]).Value));

        public static OpenLispFunc Slurp = new OpenLispFunc(x =>
            new OpenLispString(File.ReadAllText(((OpenLispString)x[0]).Value)));
    }
}