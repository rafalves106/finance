namespace Finance.Core.Domain;

public abstract class Movimentacao
{
    public Guid Id { get; private set; }
    public Guid? GrupoRecorrenciaId { get; private set; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime Data { get; private set; }
    public bool Fixa { get; private set; }
    public int? DiaFixo { get; private set; }
    public int Periodo { get; private set; }
    public TipoMovimentacao Tipo { get; protected set; }

    protected Movimentacao(string titulo, string descricao, decimal valor, DateTime data, bool fixa = false, int? diaFixo = null, int periodo = 0, Guid? grupoRecorrenciaId = null)
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

        if (fixa && (diaFixo == null || periodo <= 0))
        {
            throw new ArgumentException("O dia fixo deve ser informado para movimentações fixas junto ao período.", nameof(diaFixo));
        }

        if (!fixa && (periodo > 0 || diaFixo != null))
        {
            throw new ArgumentException("O período e o dia fixo devem ser zero para movimentações não fixas.", nameof(periodo));
        }

        if (fixa && (!diaFixo.HasValue || periodo <= 0))
        {
            throw new ArgumentException("O dia fixo e o período devem ser informados para movimentações fixas.");
        }

        if (!fixa && (periodo > 0 || diaFixo.HasValue))
        {
            throw new ArgumentException("O período e o dia fixo não devem ser preenchidos para movimentações não fixas.");
        }

        Id = Guid.NewGuid();
        Titulo = titulo;
        Descricao = descricao;
        Valor = valor;
        Data = data;
        Fixa = fixa;
        DiaFixo = diaFixo;
        Periodo = periodo;
        GrupoRecorrenciaId = grupoRecorrenciaId;
    }

    public void AtualizarDados(string titulo, string descricao, decimal valor, DateTime data, bool fixa, int? diaFixo, int periodo)
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

    if (fixa && (!diaFixo.HasValue || periodo <= 0))
    {
        throw new ArgumentException("O dia fixo e o período devem ser informados para movimentações fixas.");
    }

    if (!fixa && (periodo > 0 || diaFixo.HasValue))
    {
        throw new ArgumentException("O período e o dia fixo não devem ser preenchidos para movimentações não fixas.");
    }

    Titulo = titulo;
    Descricao = descricao;
    Valor = valor;
    Data = data;
    Fixa = fixa;
    DiaFixo = diaFixo;
    Periodo = periodo;
}

    public abstract Movimentacao ClonarComNovaData(DateTime novaData, Guid grupoId);
}