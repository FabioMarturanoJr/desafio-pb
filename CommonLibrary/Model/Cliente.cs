namespace CommonLibrary.Model
{
    public class Cliente(string nome, string cpf)
    {
        public string Nome { get; set; } = nome;
        public string Cpf { get; set; } = cpf;
        public bool SimularErroProposta { get; set; }
        public bool SimularErroCartao { get; set; }
    }
}
