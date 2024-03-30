using System;
using OpenLisp.Core.Interfaces.IoC.Contracts;

namespace OpenLisp.Core.Interfaces
{
    /// <summary>
    /// Interface to describe base logic.
    /// </summary>
    public interface IBaseLogic
    {
        /// <summary>
        /// <see cref="Guid"/> Id
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// <see cref="string"/> Name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Instance of <see cref="ILoggerContract"/> implementation. 
        /// </summary>
        ILoggerContract Logger { get; set; }
    }
}
