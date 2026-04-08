namespace Finance.Core.Domain;
public class Entrada : Movimentacao
{
    public Entrada(string titulo, string descricao, decimal valor, DateTime data, bool fixa = false, int? diaFixo = null, int periodo = 0, Guid? grupoRecorrenciaId = null) 
        : base(titulo, descricao, valor, data, fixa, diaFixo, periodo, grupoRecorrenciaId) { Tipo = TipoMovimentacao.Entrada; }

    public override Movimentacao ClonarComNovaData(DateTime novaData, Guid grupoRecorrenciaId)
    {
        return new Entrada(this.Titulo, this.Descricao, this.Valor, novaData, this.Fixa, this.DiaFixo, this.Periodo, grupoRecorrenciaId);
    }
}