using NatsExtensions.Models;

namespace NatsExtensions.Proxies
{
    /// <summary>
    ///     Proxy for isolate request-reply logic in self
    /// </summary>
    /// <typeparam name="TRequest"><see cref="Request"/></typeparam>
    /// <typeparam name="TReply"><see cref="Reply"/></typeparam>
    public interface IProxy<TRequest, TReply>
        where TRequest : Request
        where TReply : Reply
    {
        /// <summary>
        ///     Execute request
        /// </summary>
        /// <param name="request">Request for receiving data</param>
        /// <param name="subject">Receiver subject</param>
        /// <returns>Reply from other side</returns>
        TReply Execute(TRequest request, string subject);
    }
}