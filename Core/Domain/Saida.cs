namespace Finance.Core.Domain;

public class Saida : Movimentacao
{
    public Saida(string titulo, string descricao, decimal valor, DateTime data) : base(titulo, descricao, valor, data)
    {
        Tipo = TipoMovimentacao.Saida;
    }
}