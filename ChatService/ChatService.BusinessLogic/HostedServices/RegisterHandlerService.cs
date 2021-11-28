using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NATS.Client;
using NatsExtensions.Attributes;
using NatsExtensions.Extensions;
using NatsExtensions.Handlers;
using NatsExtensions.Models;
using NatsExtensions.Options;

namespace NatsExtensions.HostedServices
{
    public class RegisterHandlerService<TRequest, TReply> : IHostedService
        where TRequest : Request
        where TReply : Reply
    {
        private readonly NatsOptions _natsOptions;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _connection;

        public RegisterHandlerService(IConnection connection, IServiceProvider serviceProvider, IOptions<NatsOptions> natsOptions)
        {
            _connection = connection;
            _serviceProvider = serviceProvider;
            _natsOptions = natsOptions.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (Attribute.GetCustomAttribute(typeof(TRequest), typeof(ServiceBusAttribute)) is not ServiceBusAttribute requestSubject)
            {
                return Task.CompletedTask;
            }
            
            if (Attribute.GetCustomAttribute(typeof(TReply), typeof(ServiceBusAttribute)) is not ServiceBusAttribute replySubject)
            {
                return Task.CompletedTask;
            }

            _connection.SubscribeAsync($"{_natsOptions.Subject}.{requestSubject.Code}", (sender, args) =>
            {
                var request = args.Message.Data.ConvertFromByteArray<TRequest>();

                using var scope = _serviceProvider.CreateScope();
                var handler = scope.ServiceProvider.GetService<IHandler<TRequest, TReply>>();
                if (handler == null)
                    throw new InvalidOperationException("Handler with the same arguments not found");
                
                var reply = handler.Handle(request).Result;
                _connection.Publish($"{_natsOptions.Subject}.{replySubject.Code}", reply.ConvertToByteArray());
            });
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}