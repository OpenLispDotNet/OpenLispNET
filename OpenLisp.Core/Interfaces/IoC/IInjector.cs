using System.Collections.Generic;
using OpenLisp.Core.Interfaces.IoC.Contracts;

namespace OpenLisp.Core.Interfaces.IoC
{
    public interface IInjector : IInjectorContract
    {
        IEnumerable<IServiceContract> Services { get; set; }

        IEnumerable<IProviderContract> Providers { get; set; }

        IEnumerable<IInjectorContract> Injectors { get; set;}

        IEnumerable<IClientContract> Clients { get; set; }
    }
}