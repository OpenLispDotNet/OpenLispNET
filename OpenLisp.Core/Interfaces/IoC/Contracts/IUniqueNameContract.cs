using System.Collections.Generic;

namespace OpenLisp.Core.Interfaces.IoC.Contracts
{
    public interface IUniqueNameContract : IBaseLogic
    {
         IEnumerable<ICryptoContract> Crypto { get; set; }
    }
}