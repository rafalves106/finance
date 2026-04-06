namespace Finance.Core.Domain;

public abstract class Movimentacao
{
    public Guid Id { get; private set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }

    public TipoMovimentacao Tipo { get; protected set; }

    public DateTime Data { get; private set; }

    protected Movimentacao(string titulo, string descricao, decimal valor, DateTime data)
    {

        if (valor <= 0)
        {
            throw new ArgumentException("O valor deve ser maior que zero.", nameof(valor));
        }

        if (string.IsNullOrWhiteSpace(titulo))
        {
            throw new ArgumentException("O título não pode ser vazio.", nameof(titulo));
        }

        if (string.IsNullOrWhiteSpace(descricao))
        {
            throw new ArgumentException("A descrição não pode ser vazia.", nameof(descricao));
        }

        if (data == default)
        {
            throw new ArgumentException("A data deve ser válida.", nameof(data));
        }

        Id = Guid.NewGuid();
        Titulo = titulo;
        Descricao = descricao;
        Valor = valor;
        Data = data;
    }
}