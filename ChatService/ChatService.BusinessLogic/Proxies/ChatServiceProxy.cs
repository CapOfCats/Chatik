using ChatService.BusinessLogic;
using NatsExtensions.Proxies;
using NatsExtensions.Services;

namespace CustomerService.BusinessLogic.Proxies
{
    public class ChatServiceProxy : BaseProxy<SendMessageRequest, >
    {
        public ChatServiceProxy(INatsService natsService) : base(natsService) { }
    }
}
