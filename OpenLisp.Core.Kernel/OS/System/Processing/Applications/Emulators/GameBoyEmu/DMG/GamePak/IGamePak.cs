using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLisp.Core.Kernel.OS.System.Processing.Applications.Emulators.GameBoyEmu.DMG.GamePak
{
    interface IGamePak
    {
        byte ReadLoROM(ushort addr);
        byte ReadHiROM(ushort addr);
        void WriteROM(ushort addr, byte value);
        byte ReadERAM(ushort addr);
        void WriteERAM(ushort addr, byte value);
        void Init(byte[] ROM);
    }
}
