using CommonLibrary.Rabbit;

namespace ClienteService.Service
{
    public interface IClienteBusService
    {
        public Task Send(Message messages);
    }
}
