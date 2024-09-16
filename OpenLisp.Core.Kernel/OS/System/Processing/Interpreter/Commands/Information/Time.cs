using System;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Interpreter.Commands.Information
{
    class CommandTime : ICommand
    {
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public CommandTime(string[] commandvalues) : base(commandvalues)
        {
            Description = "to get time and date";
        }

        /// <summary>
        /// RebootCommand
        /// </summary>
        public override ReturnInfo Execute()
        {
            Console.WriteLine("The current time is:  " + Time.MonthString() + "/" + Time.DayString() + "/" + Time.YearString() + ", " + Time.TimeString(true, true, true));
            return new ReturnInfo(this, ReturnCode.OK);
        }
    }
}
