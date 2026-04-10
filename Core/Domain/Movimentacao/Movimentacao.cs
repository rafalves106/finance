namespace Finance.Core.Domain;

public abstract class Movimentacao
{
    public Guid Id { get; private set; }
    public Guid? GrupoRecorrenciaId { get; private set; }
    public Guid? InvestimentoId { get; private set; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime Data { get; private set; }
    public bool Fixa { get; private set; }
    public int Periodo { get; private set; }
    public TipoMovimentacao Tipo { get; protected set; }
    protected Movimentacao(string titulo, string descricao, decimal valor, DateTime data, bool fixa = false, int periodo = 0, Guid? grupoRecorrenciaId = null, Guid? investimentoId = null)
    {
        if (valor <= 0) throw new ArgumentException("O valor deve ser maior que zero.", nameof(valor));
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("O título não pode ser vazio.", nameof(titulo));
        if (data == default) throw new ArgumentException("A data deve ser válida.", nameof(data));
        if (fixa && periodo <= 0) throw new ArgumentException("O período deve ser informado para movimentações fixas.");
        if (!fixa && periodo > 0) throw new ArgumentException("O período não deve ser preenchido para movimentações não fixas.");

        Id = Guid.NewGuid();
        Titulo = titulo;
        Descricao = descricao;
        Valor = valor;
        Data = data;
        Fixa = fixa;
        Periodo = periodo;
        GrupoRecorrenciaId = grupoRecorrenciaId;
        InvestimentoId = investimentoId;
    }
    public void AtualizarDados(string titulo, string descricao, decimal valor, DateTime data, bool fixa, int periodo)
    {
        if (valor <= 0) throw new ArgumentException("O valor deve ser maior que zero.", nameof(valor));
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("O título não pode ser vazio.", nameof(titulo));
        if (data == default) throw new ArgumentException("A data deve ser válida.", nameof(data));

        if (fixa && periodo <= 0) throw new ArgumentException("O período deve ser informado para movimentações fixas.");
        if (!fixa && periodo > 0) throw new ArgumentException("O período não deve ser preenchido para movimentações não fixas.");

        Titulo = titulo;
        Descricao = descricao;
        Valor = valor;
        Data = data;
        Fixa = fixa;
        Periodo = periodo;
    }

    public abstract Movimentacao ClonarComNovaData(DateTime novaData, Guid grupoId);
}