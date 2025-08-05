using CommonLibrary.Rabbit;
using MassTransit;

namespace ClienteService.Consumer;

public class ClienteConsumer(ILogger<ClienteConsumer> logger) : IConsumer<Message>
{
    private readonly ILogger<ClienteConsumer> _logger = logger;

    public async Task Consume(ConsumeContext<Message> context)
    {
        await Task.Delay(1000);
        var message = context.Message;

        if(message.Texto.Contains("erro", StringComparison.CurrentCultureIgnoreCase))
        {
            _logger.LogError($"ClienteConsumer - Erro recebido: \nMensagem: {message.Texto}, HoraMensagem: {message.DataEnvio}");
        }
        else
        {
            _logger.LogWarning($"ClienteConsumer - Notificacao recebido pelo cliente com sucesso:\nMensagem: {message.Texto}, HoraMensagem: {message.DataEnvio}");
        }
    }
}
