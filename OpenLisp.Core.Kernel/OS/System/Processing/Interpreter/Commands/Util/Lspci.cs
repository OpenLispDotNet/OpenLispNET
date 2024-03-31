﻿using Microsoft.VisualBasic;
using System;
using System.Text;
using static Cosmos.HAL.PCIDevice;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Interpreter.Commands.Util
{
    class CommandLspci : ICommand
    {
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public CommandLspci(string[] commandvalues) : base(commandvalues)
        {
            Description = "list pci devices";
        }

        /// <summary>
        /// CommandLspci
        /// </summary>
        public override ReturnInfo Execute()
        {
            int count = 0;
            StringBuilder sb = new();

            foreach (Cosmos.HAL.PCIDevice device in Cosmos.HAL.PCI.Devices)
            {
                string line = Conversion.D2(device.bus) + ":" + Conversion.D2(device.slot) + ":" + Conversion.D2(device.function) + " - " + "0x" + Conversion.D4(Conversion.DecToHex(device.VendorID)) + ":0x" + Conversion.D4(Conversion.DecToHex(device.DeviceID)) + " : " + DeviceClass.GetTypeString(device) + ": " + DeviceClass.GetDeviceString(device);

                sb.AppendLine(line);
                count++;

                /*
                if (count == Kernel.console.Rows - 4)
                {
                    //Console.ReadKey();
                    count = 0;
                }
                */
            }

            Console.Write(sb.ToString());

            return new ReturnInfo(this, ReturnCode.OK);
        }
    }
}
