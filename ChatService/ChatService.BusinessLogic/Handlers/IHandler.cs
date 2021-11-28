using System.Threading.Tasks;
using NatsExtensions.Models;

namespace NatsExtensions.Handlers
{
    /// <summary>
    ///     Interface for nats handler
    /// </summary>
    /// <typeparam name="TRequest"><see cref="Request"/></typeparam>
    /// <typeparam name="TReply"><see cref="Reply"/></typeparam>
    public interface IHandler<TRequest, TReply>
        where TRequest : Request
        where TReply : Reply
    {
        /// <summary>
        ///     Handle received request
        /// </summary>
        /// <param name="request">Request from remote subject</param>
        /// <returns>Handler reply</returns>
        Task<TReply> Handle(TRequest request);
    }
}