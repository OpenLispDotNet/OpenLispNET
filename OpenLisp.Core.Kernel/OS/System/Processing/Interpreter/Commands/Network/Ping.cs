﻿using System;
using Sys = Cosmos.System;
using Cosmos.System.Network;
using System.Collections.Generic;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4.UDP.DNS;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Interpreter.Commands.Network
{
    class CommandPing : ICommand
    {
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public CommandPing(string[] commandvalues) : base(commandvalues, CommandType.Network)
        {
            Description = "to send ping requests to an IP or domain name using ICMP";
        }

        /// <summary>
        /// CommandEcho
        /// </summary>
        /// <param name="arguments">Arguments</param>
        public override ReturnInfo Execute(List<string> arguments)
        {
            int PacketSent = 0;
            int PacketReceived = 0;
            int PacketLost = 0;
            int PercentLoss;

            Address source;
            Address destination = Address.Parse(arguments[0]);

            if (destination != null)
            {
                source = IPConfig.FindNetwork(destination);
            }
            else //Make a DNS request if it's not an IP
            {
                var xClient = new DnsClient();
                xClient.Connect(DNSConfig.DNSNameservers[0]);
                xClient.SendAsk(arguments[0]);
                destination = xClient.Receive();
                xClient.Close();

                if (destination == null)
                {
                    return new ReturnInfo(this, ReturnCode.ERROR, "Failed to get DNS response for " + arguments[0]);
                }

                source = IPConfig.FindNetwork(destination);
            }

            try
            {
                Console.WriteLine("Sending ping to " + destination.ToString());

                var xClient = new ICMPClient();
                xClient.Connect(destination);

                for (int i = 0; i < 4; i++)
                {
                    xClient.SendEcho();

                    PacketSent++;

                    var endpoint = new EndPoint(Address.Zero, 0);

                    int second = xClient.Receive(ref endpoint, 4000);

                    if (second == -1)
                    {
                        Console.WriteLine("Destination host unreachable.");
                        PacketLost++;
                    }
                    else
                    {
                        if (second < 1)
                        {
                            Console.WriteLine("Reply received from " + endpoint.Address.ToString() + " time < 1s");
                        }
                        else if (second >= 1)
                        {
                            Console.WriteLine("Reply received from " + endpoint.Address.ToString() + " time " + second + "s");
                        }

                        PacketReceived++;
                    }
                }

                xClient.Close();
            }
            catch
            {
                return new ReturnInfo(this, ReturnCode.ERROR, "Ping process error.");
            }

            PercentLoss = 25 * PacketLost;

            Console.WriteLine();
            Console.WriteLine("Ping statistics for " + destination.ToString() + ":");
            Console.WriteLine("    Packets: Sent = " + PacketSent + ", Received = " + PacketReceived + ", Lost = " + PacketLost + " (" + PercentLoss + "% loss)");

            return new ReturnInfo(this, ReturnCode.OK);
        }

        /// <summary>
        /// Print /help information
        /// </summary>
        public override void PrintHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine(" - ping {ip}");
            Console.WriteLine(" - ping {domain_name}");
        }
    }
}
