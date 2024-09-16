using Cosmos.Core;

namespace OpenLisp.Core.Kernel.OS.Core
{
    public class Memory
    {
        public static uint TotalMemory = CPU.GetAmountOfRAM();
        public uint FreePercentage;
        public uint UsedPercentage = (GetUsedMemory() * 100) / TotalMemory;
        public uint FreeMemory = TotalMemory - GetUsedMemory();
        private const uint div = 1048576;

        public Memory()
        {
            this.Monitor();
        }

        public static void GetTotalMemory()
        {
            TotalMemory = CPU.GetAmountOfRAM() + 1;
        }

        public void Monitor()
        {
            GetTotalMemory();
            FreeMemory = TotalMemory - GetUsedMemory();
            UsedPercentage = (GetUsedMemory() * 100) / TotalMemory;
            FreePercentage = 100 - UsedPercentage;
        }

        public static uint GetFreeMemory()
        {
            return TotalMemory - GetUsedMemory();
        }

        public static uint GetUsedMemory()
        {
            uint UsedRAM = CPU.GetEndOfKernel() + 1024;
            return UsedRAM / div;
        }
    }
}
