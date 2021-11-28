using NatsExtensions.Models;

namespace NatsExtensions.Services
{
    public interface INatsService
    {
        /// <summary>
        ///     Send request to the remote handler
        /// </summary>
        /// <param name="request">Request body</param>
        /// <param name="subject">Subject, that handles request</param>
        /// <typeparam name="TRequest">Request type</typeparam>
        /// <typeparam name="TReply">Reply type</typeparam>
        /// <returns>Reply body</returns>
        TReply RequestReply<TRequest, TReply>(TRequest request, string subject)
            where TRequest : Request
            where TReply   : Reply;
    }
}