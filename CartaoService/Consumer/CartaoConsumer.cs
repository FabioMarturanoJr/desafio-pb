using CartaoService.Service;
using CommonLibrary.Rabbit;
using MassTransit;

namespace CartaoService.Consumer;

public class CartaoConsumer(ILogger<CartaoConsumer> logger, ICartaoBusService busService) : IConsumer<Message>
{
    private readonly ILogger<CartaoConsumer> _logger = logger;
    private readonly ICartaoBusService _busService = busService;

    public async Task Consume(ConsumeContext<Message> context)
    {
        await Task.Delay(1000);
        var message = context.Message;

        if (message.SimularErroCartao)
        {
            _logger.LogError($"MessageConsumer - Erro ao processar a mensagem :\nMensagem: {message.Texto}, HoraMensagem: {message.DataEnvio}");
            await _busService.SendError(message);
        }
        else
        {
            await _busService.Send(new Message("Cliente cadastrado com sucesso", message.DataEnvio));
        }
    }
}