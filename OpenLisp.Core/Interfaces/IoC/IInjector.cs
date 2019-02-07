using System.Collections.Generic;
using OpenLisp.Core.Interfaces.IoC.Contracts;

namespace OpenLisp.Core.Interfaces.IoC
{
    /// <summary>
    /// Interface to describe an injector implementation.
    /// </summary>
    public interface IInjector : IInjectorContract
    {
        /// <summary>
        /// <see cref="IEnumerable{IServiceContract}"/> collection of Services contracts.
        /// </summary>
        IEnumerable<IServiceContract> Services { get; set; }

        /// <summary>
        /// <see cref="IEnumerable{IProviderContract}"/> collection of Providers contracts.
        /// </summary>
        IEnumerable<IProviderContract> Providers { get; set; }

        /// <summary>
        /// <see cref="IEnumerable{IInjectorContract}"/> collection of Injectors contracts.
        /// </summary>
        IEnumerable<IInjectorContract> Injectors { get; set;}

        /// <summary>
        /// <see cref="IEnumerable{IClientContract}"/> collection of Clients contracts.
        /// </summary>
        IEnumerable<IClientContract> Clients { get; set; }
    }
}