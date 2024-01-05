using System.Collections;
using System.Collections.Generic;

namespace OpenLisp.Core.Interfaces.IoC.Providers
{
    /// <summary>
    /// Interface describing a network provider.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    public interface INetworkProvider<T1, T2, T3, T4> : IBaseLogic
    {
        /// <summary>
        /// <see cref="IEnumerable{T1}"/> collection of Adapter.
        /// </summary>
        IEnumerable<T1> Adapters { get; set; }

        /// <summary>
        /// <see cref="IEnumerable{T2}"/> collection of Networks.
        /// </summary>
        IEnumerable<T2> Networks { get; set; }

        /// <summary>
        /// <see cref="IEnumerable{T3}"/> collection of Input Streams.
        /// </summary>
        IEnumerable<T3> InputStreams { get; set; } 

        /// <summary>
        /// <see cref="IEnumerable{T4}"/> collection of Output Streams
        /// </summary>
        IEnumerable<T4> OutputStreams { get; set; }

        /// <summary>
        /// <see cref="IEnumerable{Object}"/> collection of Sockets.
        /// </summary>
        IEnumerable<object> Sockets { get; set; }

        /// <summary>
        /// <see cref="IEnumerable{Object}"/> collection of Hosts.
        /// </summary>
        IEnumerable<object> Hosts { get; set; }

        /// <summary>
        /// Method to manipulate a Socket.
        /// </summary>
        /// <param name="networkProvider"></param>
        /// <returns></returns>
        INetworkProvider<T1, T2, T3, T4> Socket(INetworkProvider<T1, T2, T3, T4> networkProvider);

        /// <summary>
        /// Method to perform a host lookup operation.
        /// </summary>
        /// <param name="networkProvider"></param>
        /// <param name="hosts"></param>
        /// <returns></returns>
        INetworkProvider<T1, T2, T3, T4> HostLookup(INetworkProvider<T1, T2, T3, T4> networkProvider, 
            IEnumerable<object> hosts);

        /// <summary>
        /// Method to perform a host lookup operation via reverse DNS.
        /// </summary>
        /// <param name="networkProvider"></param>
        /// <param name="hosts"></param>
        /// <returns></returns>
        INetworkProvider<T1, T2, T3, T4> ReverseHostLookup(INetworkProvider<T1, T2, T3, T4> networkProvider, 
            IEnumerable<object> hosts);

        /// <summary>
        /// Method to manipulate a Command to an implementation of <see cref="INetworkProvider{T1, T2, T3, T4}"/>.
        /// </summary>
        /// <typeparam name="T5"></typeparam>
        /// <param name="networkProvider"></param>
        /// <returns></returns>
        INetworkProvider<T1, T2, T3, T4> Command<T5>(INetworkProvider<T1, T2, T3, T4> networkProvider);
    }
}
