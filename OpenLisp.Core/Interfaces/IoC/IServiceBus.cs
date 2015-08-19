using System.Collections.Generic;
using OpenLisp.Core.Interfaces.IoC.Contracts;

namespace OpenLisp.Core.Interfaces.IoC
{
    public interface IServiceBus : IBaseLogic
    {
        IEnumerable<IEventSourceContract> EventSources { get; set; }

        IDispatchContract Dispatch { get; set; }
        
        IQueueContract IncomingQueue { get; set; }

        IQueueContract OutgoingQueue { get; set; }
    }
}