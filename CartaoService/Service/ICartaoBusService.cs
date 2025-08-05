using CommonLibrary.Rabbit;

namespace CartaoService.Service;

public interface ICartaoBusService
{
    public Task Send(Message message);
    Task SendError(Message message);
}
