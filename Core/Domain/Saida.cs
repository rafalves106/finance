namespace Finance.Core.Domain;

public class Saida : Movimentacao
{
    public Saida(string titulo, string descricao, decimal valor, DateTime data, bool fixa = false, int periodo = 0, Guid? grupoRecorrenciaId = null) 
        : base(titulo, descricao, valor, data, fixa, periodo, grupoRecorrenciaId) { Tipo = TipoMovimentacao.Saida; }

    public override Movimentacao ClonarComNovaData(DateTime novaData, Guid grupoRecorrenciaId)
    {
        return new Saida(this.Titulo, this.Descricao, this.Valor, novaData, this.Fixa, this.Periodo, grupoRecorrenciaId);
    }
}