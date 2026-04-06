using Finance.Core.Domain;

namespace Finance.Core.Repositories;

public interface IMovimentacaoRepository
{
    Guid Adicionar(Movimentacao movimentacao);
    IEnumerable<Movimentacao> Listar();
    void Remover(Movimentacao movimentacao);
    void Atualizar(Movimentacao movimentacao);
    Movimentacao? ObterPorId(Guid id);
    IEnumerable<Entrada> ListarEntradas();
    IEnumerable<Saida> ListarSaidas();
    IEnumerable<Movimentacao> ListarPorPeriodo(DateTime dataInicio, DateTime dataFim);
}