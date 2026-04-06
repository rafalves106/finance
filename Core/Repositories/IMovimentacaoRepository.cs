using Finance.Core.Domain;

namespace Finance.Core.Repositories;

public interface IMovimentacaoRepository
{
    void Adicionar(Movimentacao movimentacao);
    IEnumerable<Movimentacao> Listar();
    void Remover(Movimentacao movimentacao);
    void Atualizar(Guid id, Movimentacao movimentacao);
    Movimentacao? ObterPorId(Guid id);
    IEnumerable<Entrada> ListarEntradas();
    IEnumerable<Saida> ListarSaidas();
}