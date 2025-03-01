using MassTransit;
using Pacagroup.Ecommerce.Application.Interface.Infraestructure;

namespace Pacagroup.Ecommerce.Infraestructure.EventBus
{
    public class EventBusRabbitMQ : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBusRabbitMQ(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async void Publish<T>(T @event)
        {
            await _publishEndpoint.Publish(@event!);
        }
    }
}
