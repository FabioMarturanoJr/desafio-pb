using CommonLibrary.Rabbit;

namespace PropostaService.Service
{
    public interface IPropostaBusService
    {
        public Task Send(Message messages);
        Task SendError(Message message);
    }
}
