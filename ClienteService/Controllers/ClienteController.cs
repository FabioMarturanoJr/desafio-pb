using ClienteService.Service;
using CommonLibrary.Model;
using CommonLibrary.Rabbit;
using Microsoft.AspNetCore.Mvc;

namespace ClienteService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController(IClienteBusService clienteBus) : ControllerBase
    {
        private readonly IClienteBusService _clienteBus = clienteBus;
        [HttpPost]
        public async Task<ActionResult<string>> Cadastrar([FromBody] Cliente cliente)
        {
            await _clienteBus.Send(new Message("Envio do Cliente para Proposta", DateTime.Now, cliente.SimularErroProposta, cliente.SimularErroCartao));
            return Ok($"Clinete {cliente.Nome} enviado para cadastrado com sucesso");
        }
    }
}
