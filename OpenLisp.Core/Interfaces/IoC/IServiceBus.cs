using System.Collections.Generic;
using OpenLisp.Core.Interfaces.IoC.Contracts;
using OpenLisp.Core.Interfaces.IoC.Providers;

namespace OpenLisp.Core.Interfaces.IoC
{
    public interface IServiceBus : IBaseLogic
    {
        IEnumerable<IEventSourceContract> EventSources { get; set; }

        IDispatchContract Dispatch { get; set; }
        
        IQueueContract IncomingQueue { get; set; }

        IQueueContract OutgoingQueue { get; set; }

        IEnumerable<IServiceProvider> Services { get; set; }

        IEnumerable<IProvider> Providers { get; set; }

        IEnumerable<IContract> Contracts { get; set; }
    }
}