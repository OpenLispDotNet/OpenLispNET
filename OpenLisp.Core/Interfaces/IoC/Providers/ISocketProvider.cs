using System.Collections.Generic;

namespace OpenLisp.Core.Interfaces.IoC.Providers
{
    public interface ISocketProvider : IBaseLogic
    {
        object Origin { get; set; }

        object Destination { get; set; }

        IEnumerable<object> Streams { get; set; } 
    }
}