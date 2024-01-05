namespace OpenLisp.Core.Interfaces.IoC.Providers
{
    /// <summary>
    /// Interface to describe an object provider.
    /// </summary>
    public interface IObjectProvider : IBaseLogic
    {
        /// <summary>
        /// Late-binding Object reference.
        /// </summary>
        dynamic Object { get; set; }
    }
}
