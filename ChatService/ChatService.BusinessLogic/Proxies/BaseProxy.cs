using NatsExtensions.Models;
using NatsExtensions.Services;

namespace NatsExtensions.Proxies
{
    /// <summary>
    ///     Proxy class with base logic
    /// </summary>
    /// <typeparam name="TRequest"><see cref="Request"/></typeparam>
    /// <typeparam name="TReply"><see cref="Reply"/></typeparam>
    public class BaseProxy<TRequest, TReply> : IProxy<TRequest, TReply>
        where TRequest : Request
        where TReply : Reply
    {
        private readonly INatsService _natsService;

        /// <inheritdoc cref="BaseProxy{TRequest,TReply}"/>
        public BaseProxy(INatsService natsService) =>
            _natsService = natsService;

        /// <inheritdoc/>
        public virtual TReply Execute(TRequest request, string subject) =>
            _natsService.RequestReply<TRequest, TReply>(request, subject);
    }
}