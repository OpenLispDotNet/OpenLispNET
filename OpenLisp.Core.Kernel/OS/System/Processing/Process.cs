﻿namespace OpenLisp.Core.Kernel.OS.System.Processing
{
    public enum ProcessType
    {
        KernelComponent,
        Driver,
        Utility,
        Program,
    }

    public abstract class Process
    {
        public string Name { get; protected set; }
        public uint ID { get; protected set; }
        public ProcessType Type { get; protected set; }
        public bool Initialized { get; protected set; }
        public bool Running { get; private set; }

        private static string[] TypeNames = new string[]
        {
            "KernelComponent",
            "Driver",
            "Utility",
            "Program",
            "Unknown",
        };

        public Process(string name, ProcessType type)
        {
            Name = name;
            Type = type;
            ID = 0;
            Running = false;
        }

        public virtual void Initialize()
        {
            if (Initialized)
            {
                return;
            }
            Initialized = true;
        }

        public virtual void Start()
        {
            if (Running)
            {
                return;
            }
            Running = true;
        }

        public virtual void Stop()
        {
            if (!Running)
            {
                return;
            }
            Running = false;
        }

        public virtual void Update() { }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetID(uint id)
        {
            ID = id;
        }

        public void SetType(ProcessType type)
        {
            Type = type;
        }

        public static string GetServiceTypeString(ProcessType type)
        {
            switch (type)
            {
                case ProcessType.KernelComponent:
                    {
                        return TypeNames[0];
                    }
                case ProcessType.Driver:
                    {
                        return TypeNames[1];
                    }
                case ProcessType.Utility:
                    {
                        return TypeNames[2];
                    }
                case ProcessType.Program:
                    {
                        return TypeNames[3];
                    }
                default:
                    {
                        return TypeNames[4];
                    }
            }
        }
    }
}
