using System;
using NATS.Client;
using NatsExtensions.Attributes;
using NatsExtensions.Extensions;
using NatsExtensions.Models;

namespace NatsExtensions.Services
{
    public class NatsService : INatsService
    {
        private IConnection _connection;
        private const int Timeout = 5 * 1000;
        
        public NatsService(IConnection connection)
        {
            _connection = connection;
        }
        
        public TReply RequestReply<TRequest, TReply>(TRequest request, string subject) 
            where TRequest : Request 
            where TReply   : Reply
        {
            if (Attribute.GetCustomAttribute(typeof(TRequest), typeof(ServiceBusAttribute)) is not ServiceBusAttribute requestSubject)
            {
                throw new InvalidOperationException("Request needs 'service bus' attribute for data sending");
            }
            
            if (Attribute.GetCustomAttribute(typeof(TReply), typeof(ServiceBusAttribute)) is not ServiceBusAttribute replySubject)
            {
                throw new InvalidOperationException("Reply needs 'service bus' attribute for data receiving");
            }

            var subscription = _connection.SubscribeSync($"{subject}.{replySubject.Code}");
            if (!subscription.IsValid)
            {
                throw new InvalidOperationException("Cannot connect to NATS");
            }

            var data = request.ConvertToByteArray();
            _connection.Publish($"{subject}.{requestSubject.Code}", data);

            var message = subscription.NextMessage(Timeout).Data;
            return message.ConvertFromByteArray<TReply>();
        }
    }
}