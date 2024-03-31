using IL2CPU.API.Attribs;
using Cosmos.HAL;
using System.IO;
using Cosmos.System;

namespace OpenLisp.Core.Kernel.Plugs
{
    [Plug(Target = typeof(Cosmos.System.Global))]
    public static class Global
    {
        // TODO: continue adding exceptions to the list, as HAL and Core would be documented.
        /// <summary>
        /// Initializes the console, screen and keyboard.
        /// </summary>
        /// <param name="textScreen">A screen device.</param>
        public static void Init(TextScreenBase textScreen, bool initScrollWheel = true, bool initPS2 = true, bool initNetwork = true, bool ideInit = true)
        {
            // We must init Console before calling Inits.
            // This is part of the "minimal" boot to allow output.
            Cosmos.System.Global.Console = new Console(textScreen);

            Kernel.TextmodeConsole = new Graphics.UI.CUI.Console(textScreen);

            CustomConsole.WriteLineInfo("Starting the OpenLisp.NET Machine v" + Kernel.Version + "-" + Kernel.Revision);

            CustomConsole.WriteLineInfo("Initializing the Hardware Abstraction Layer (HAL)...");
            Cosmos.HAL.Global.Init(textScreen, initScrollWheel, initPS2, initNetwork, ideInit);

            CustomConsole.WriteLineInfo("Initializing the network stack...");
            Cosmos.System.Network.NetworkStack.Initialize();

            Cosmos.System.Global.NumLock = false;
            Cosmos.System.Global.CapsLock = false;
            Cosmos.System.Global.ScrollLock = false;
        }
    }
}
