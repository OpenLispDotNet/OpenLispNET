using System.Collections;
using System.Collections.Generic;

namespace OpenLisp.Core.Interfaces.IoC.Providers
{
    public interface INetworkProvider<T1, T2, T3, T4> : IBaseLogic
    {
        IEnumerable<T1> Adapters { get; set; }

        IEnumerable<T2> Networks { get; set; }

        IEnumerable<T3> InputStreams { get; set; } 

        IEnumerable<T4> OutputStreams { get; set; }

        IEnumerable<object> Sockets { get; set; }

        IEnumerable<object> Hosts { get; set; }

        INetworkProvider<T1, T2, T3, T4> Socket(INetworkProvider<T1, T2, T3, T4> networkProvider);

        INetworkProvider<T1, T2, T3, T4> HostLookup(INetworkProvider<T1, T2, T3, T4> networkProvider, 
            IEnumerable<object> hosts);

        INetworkProvider<T1, T2, T3, T4> ReverseHostLookup(INetworkProvider<T1, T2, T3, T4> networkProvider, 
            IEnumerable<object> hosts);

        INetworkProvider<T1, T2, T3, T4> Command<T5>(INetworkProvider<T1, T2, T3, T4> networkProvider);
    }
}