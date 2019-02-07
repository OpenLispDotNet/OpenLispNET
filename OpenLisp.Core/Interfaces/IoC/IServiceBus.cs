using System.Collections.Generic;
using OpenLisp.Core.Interfaces.IoC.Contracts;
using OpenLisp.Core.Interfaces.IoC.Providers;

namespace OpenLisp.Core.Interfaces.IoC
{
    /// <summary>
    /// Interface to describe a service bus implementation.
    /// </summary>
    public interface IServiceBus : IBaseLogic
    {
        /// <summary>
        /// <see cref="IEnumerable{IEventSourceContract}"/> collection of Event Sources contracts.
        /// </summary>
        IEnumerable<IEventSourceContract> EventSources { get; set; }

        /// <summary>
        /// Instance of an implementation of <see cref="IDispatchContract"/>.
        /// </summary>
        IDispatchContract Dispatch { get; set; }
        
        /// <summary>
        /// Instance of an implementation of <see cref="IQueueContract"/> for incoming queues.
        /// </summary>
        IQueueContract IncomingQueue { get; set; }

        /// <summary>
        /// Instance of an implementation of <see cref="IQueueContract"/> for outgoing queues.
        /// </summary>
        IQueueContract OutgoingQueue { get; set; }

        /// <summary>
        /// <see cref="IEnumerable{IServiceProvider}"/> collection of Service Provider contracts.
        /// </summary>
        IEnumerable<IServiceProvider> Services { get; set; }

        /// <summary>
        /// <see cref="IEnumerable{IProvider}"/> collections of Providers.
        /// </summary>
        IEnumerable<IProvider> Providers { get; set; }

        /// <summary>
        /// <see cref="IEnumerable{IContract}"/> collection of Contracts.
        /// </summary>
        IEnumerable<IContract> Contracts { get; set; }
    }
}