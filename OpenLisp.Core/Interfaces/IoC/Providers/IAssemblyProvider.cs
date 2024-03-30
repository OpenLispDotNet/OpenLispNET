using System;

namespace OpenLisp.Core.Interfaces.IoC.Providers
{
    /// <summary>
    /// Interface describing an assembly provider.
    /// </summary>
    public interface IAssemblyProvider : IBaseLogic
    {
        /// <summary>
        /// The assembly name.
        /// </summary>
        string AssemblyName { get; set; }

        /// <summary>
        /// The assembly namespace.
        /// </summary>
        string NameSpace { get; set; }

        /// <summary>
        /// The assembly version.
        /// </summary>
        string Version { get; set; }
    }
}
