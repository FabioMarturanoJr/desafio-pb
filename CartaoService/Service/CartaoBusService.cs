using CommonLibrary.Configs;
using CommonLibrary.Rabbit;
using MassTransit;
using Microsoft.Extensions.Options;

namespace CartaoService.Service;

public class CartaoBusService(IBus bus, ILogger<CartaoBusService> logger, IOptions<MassTransitConfigs> MassTransitConfigs) : ICartaoBusService
{
    private readonly IBus _bus = bus;
    private readonly ILogger<CartaoBusService> _logger = logger;
    private readonly MassTransitConfigs _MassTransitConfigs = MassTransitConfigs.Value;

    public async Task Send(Message message)
    {
        var endpoint = await _bus.GetSendEndpoint(new Uri($"{_MassTransitConfigs.host}/{Queues.Cliente}"));
        await endpoint.Send(message);
        _logger.LogInformation($"Mensagem: \"{message.Texto}\" Enviada Com Sucesso");
    }

    public async Task SendError(Message message)
    {
        var endpoint = await _bus.GetSendEndpoint(new Uri($"{_MassTransitConfigs.host}/{Queues.Cliente}"));
        await endpoint.Send(new Message("Error Processamento do cartao", message.DataEnvio));
        _logger.LogInformation($"Mensagem: \"{message.Texto}\" Enviada Com Sucesso");
    }
}