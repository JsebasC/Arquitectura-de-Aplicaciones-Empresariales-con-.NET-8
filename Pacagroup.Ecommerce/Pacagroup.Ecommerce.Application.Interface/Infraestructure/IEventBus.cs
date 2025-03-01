namespace Pacagroup.Ecommerce.Application.Interface.Infraestructure
{
    public interface IEventBus
    {
        void Publish<T>(T @event);
    }
}
