namespace Finance.Core.Domain;
public class Entrada : Movimentacao
{
    public Entrada(string titulo, string descricao, decimal valor, DateTime data) : base(titulo, descricao, valor, data)
    {
        Tipo = TipoMovimentacao.Entrada;
    }
}