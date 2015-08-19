using System;
using OpenLisp.Core.Interfaces.IoC.Contracts;

namespace OpenLisp.Core.Interfaces
{
    public interface IBaseLogic
    {
        Guid Id { get; set; }

        string Name { get; set; }

        ILoggerContract Logger { get; set; }
    }
}