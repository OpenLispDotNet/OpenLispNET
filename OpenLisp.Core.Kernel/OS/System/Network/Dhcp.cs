using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using System;
using System.Collections.Generic;
using System;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4.UDP.DHCP;

namespace OpenLisp.Core.Kernel.OS.System.Network
{
    public static class Dhcp
    {
        public static void Release()
        {
            var xClient = new DHCPClient();
            xClient.SendReleasePacket();
            xClient.Close();

            NetworkConfiguration.ClearConfigs();

            Kernel.NetworkConnected = false;
            Explorer.Taskbar.MarkDirty();
        }

        public static bool Ask()
        {
            var xClient = new DHCPClient();
            if (xClient.SendDiscoverPacket() != -1)
            {
                xClient.Close();
                Console.WriteLine("Configuration applied! Your local IPv4 Address is " + NetworkConfiguration.CurrentAddress + ".");
                Kernel.NetworkConnected = true;
                return true;
            }
            else
            {
                NetworkConfiguration.ClearConfigs();

                xClient.Close();
                return false;
            }
        }
    }
}
