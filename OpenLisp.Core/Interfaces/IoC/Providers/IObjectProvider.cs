namespace OpenLisp.Core.Interfaces.IoC.Providers
{
    public interface IObjectProvider : IBaseLogic
    {
         dynamic Object { get; set; }
    }
}