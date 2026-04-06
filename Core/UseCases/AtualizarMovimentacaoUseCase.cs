using Finance.Core.Domain;
using Finance.Core.Repositories;

namespace Finance.Core.UseCases;

public class AtualizarMovimentacaoUseCase(IMovimentacaoRepository _movimentacaoRepository)
{
    public void Executar(Guid id, Movimentacao movimentacao)
    {
        var movimentacaoExistente = _movimentacaoRepository.ObterPorId(id);

        if (movimentacaoExistente == null)
        {
            throw new Exception("Movimentação não encontrada.");
        }

        movimentacaoExistente.Titulo = movimentacao.Titulo;
        movimentacaoExistente.Descricao = movimentacao.Descricao;
        movimentacaoExistente.Valor = movimentacao.Valor;
        movimentacaoExistente.Data = movimentacao.Data;

        _movimentacaoRepository.Atualizar(movimentacaoExistente);
    }
}