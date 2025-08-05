using CommonLibrary.Rabbit;
using MassTransit;
using PropostaService.Service;

namespace PropostaService.Consumer;

public class PropostaConsumer(ILogger<PropostaConsumer> logger, IPropostaBusService busService) : IConsumer<Message>
{
    private readonly ILogger<PropostaConsumer> _logger = logger;
    private readonly IPropostaBusService _busService = busService;

    public async Task Consume(ConsumeContext<Message> context)
    {
        await Task.Delay(1000);
        var message = context.Message;

        if (message.SimularErroProposta)
        {
            _logger.LogError($"MessageConsumer - Erro ao processar a mensagem :\nMensagem: {message.Texto}, HoraMensagem: {message.DataEnvio}");
            await _busService.SendError(message);
        } else
        {
            await _busService.Send(new Message("Envio do cliente para gerar cartao", message.DataEnvio, message.SimularErroProposta, message.SimularErroCartao));
        }
    }
}