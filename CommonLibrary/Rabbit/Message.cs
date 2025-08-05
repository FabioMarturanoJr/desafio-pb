namespace CommonLibrary.Rabbit;

public class Message(string texto, DateTime dataEnvio, bool simularErroProposta = false, bool simularErroCartao = false)
{
    public string Texto { get; set; } = texto;
    public DateTime DataEnvio { get; set; } = dataEnvio;
    public bool SimularErroProposta { get; set; } = simularErroProposta;
    public bool SimularErroCartao { get; set; } = simularErroCartao;
}
