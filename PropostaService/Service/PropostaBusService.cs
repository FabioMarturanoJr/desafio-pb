using CommonLibrary.Configs;
using CommonLibrary.Rabbit;
using MassTransit;
using Microsoft.Extensions.Options;

namespace PropostaService.Service;

public class PropostaBusService(IBus bus, ILogger<PropostaBusService> logger, IOptions<MassTransitConfigs> MassTransitConfigs) : IPropostaBusService
{
    private readonly IBus _bus = bus;
    private readonly ILogger<PropostaBusService> _logger = logger;
    private readonly MassTransitConfigs _MassTransitConfigs = MassTransitConfigs.Value;

    public async Task Send(Message message)
    {
        var endpoint = await _bus.GetSendEndpoint(new Uri($"{_MassTransitConfigs.host}/{Queues.Cartao}"));
        await endpoint.Send(message);
        _logger.LogInformation($"Mensagem: \"{message.Texto}\" Enviada Com Sucesso");
    }

    public async Task SendError(Message message)
    {
        var endpoint = await _bus.GetSendEndpoint(new Uri($"{_MassTransitConfigs.host}/{Queues.Cliente}"));
        await endpoint.Send(new Message("Erro no processamento da proposta", message.DataEnvio));
        _logger.LogInformation($"Mensagem: \"{message.Texto}\" Enviada Com Sucesso");
    }
}
