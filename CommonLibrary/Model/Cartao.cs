namespace CommonLibrary.Model;

public class Cartao(int id, int idCliente)
{
    public int Id { get; set; } = id;
    public int IdCliente { get; set; } = idCliente;
}
