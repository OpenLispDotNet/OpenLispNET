using System.Collections.Generic;

namespace OpenLisp.Core.Interfaces.IoC.Contracts
{
    /// <summary>
    /// Interface to describe the unique name contract.
    /// </summary>
    public interface IUniqueNameContract : IBaseLogic
    {
        /// <summary>
        /// <see cref="IEnumerable{ICryptoContract}"/> collection.
        /// </summary>
        IEnumerable<ICryptoContract> Crypto { get; set; }
    }
}
