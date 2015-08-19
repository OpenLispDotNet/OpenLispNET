using System;

namespace OpenLisp.Core.Interfaces.IoC.Providers
{
    public interface IAssemblyProvider : IBaseLogic
    {
        string AssemblyName { get; set; }

        string NameSpace { get; set; }

        string Version { get; set; }
    }
}