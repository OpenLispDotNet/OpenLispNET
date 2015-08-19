using System;

namespace OpenLisp.Core.Interfaces
{
    public interface IBaseLogic
    {
        Guid Id { get; set; }

        string Name { get; set; }
    }
}