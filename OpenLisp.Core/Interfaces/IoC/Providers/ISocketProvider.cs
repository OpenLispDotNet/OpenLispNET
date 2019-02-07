using System.Collections.Generic;

namespace OpenLisp.Core.Interfaces.IoC.Providers
{
    /// <summary>
    /// Interface to descrive a socket provider.
    /// </summary>
    public interface ISocketProvider : IBaseLogic
    {
        /// <summary>
        /// Socket origin reference.
        /// </summary>
        object Origin { get; set; }

        /// <summary>
        /// Socjet destination reference.
        /// </summary>
        object Destination { get; set; }

        /// <summary>
        /// <see cref="IEnumerable{Object}"/> collection of streams over an individual instance
        /// of an implementation of <see cref="ISocketProvider"/>.
        /// </summary>
        IEnumerable<object> Streams { get; set; } 
    }
}