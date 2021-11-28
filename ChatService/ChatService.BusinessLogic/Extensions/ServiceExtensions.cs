using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NATS.Client;
using NatsExtensions.Extensions;
using NatsExtensions.Options;
using NatsExtensions.Services;
using CustomerService.BusinessLogic.Proxies;

namespace ChatService.BusinessLogic.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<DBContext>(builder =>
                builder.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("ProductService.BusinessLogic")));
        }

        public static IServiceCollection AddNats(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddNatsExtensions(builder =>
            {
                builder.Subject = configuration.GetSection("Nats")["Subject"];
                builder.ConnectionString = configuration.GetConnectionString("NatsConnection");
            })
            .AddNatsHandlers()
            .AddNatsProxies();
        }
        
        private static IServiceCollection AddNatsHandlers(this IServiceCollection services) =>
            services.AddNatsHandler<
                DeleteMessagesRequest,
                EditMessageRequest,
                GetMessagesRequest,
                SendMessageRequest,
                UserTypingRequest,
                MessageService,
                UserConnectionService,
                >();
                //Add Replies
        private static IServiceCollection AddNatsProxies(this IServiceCollection services) =>
            services.AddNatsProxy<
                GetOrdersByCustomerIdRequest,
                GetOrdersByCustomerIdReply,
                ChatServiceProxy>();
    }
}