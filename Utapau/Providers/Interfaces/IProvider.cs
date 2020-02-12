namespace Utapau.Providers.Interfaces
{
    /// <summary>
    /// Represents provider of <typeparamref name="TService" /> instance
    /// </summary>
    /// <typeparam name="TService">The type of service to resolve by provider</typeparam>
    public interface IProvider<out TService>
    {
        /// <summary>
        /// <typeparam name="TService"/> instance from current scope
        /// </summary>
        TService Instance { get; }
    }
}