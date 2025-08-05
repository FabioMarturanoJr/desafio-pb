using CommonLibrary.Configs;
using CommonLibrary.Rabbit;
using MassTransit;
using Microsoft.Extensions.Options;

namespace ClienteService.Service
{
    public class ClienteBusService(IBus bus, ILogger<ClienteBusService> logger, IOptions<MassTransitConfigs> MassTransitConfigs) : IClienteBusService
    {
        private readonly IBus _bus = bus;
        private readonly ILogger<ClienteBusService> _logger = logger;
        private readonly MassTransitConfigs _MassTransitConfigs = MassTransitConfigs.Value;

        public async Task Send(Message message)
        {
            var endpoint = await _bus.GetSendEndpoint(new Uri($"{_MassTransitConfigs.host}/{Queues.Proposta}"));
            await endpoint.Send(message);
            _logger.LogInformation($"ClienteBusService - Mensagem: \"{message.Texto}\" Enviada Com Sucesso");
        }
    }
}
