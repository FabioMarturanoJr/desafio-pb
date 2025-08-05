namespace CommonLibrary.Model;

public class Proposta(int idClinte, bool propAceita)
{
    public int IdCLinte { get; set; } = idClinte;
    public bool PropostaAceita { get; set; } = propAceita;
}
