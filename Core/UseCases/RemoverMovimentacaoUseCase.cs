using Finance.Core.Domain;
using Finance.Core.Repositories;

namespace Finance.Core.UseCases;

public class RemoverMovimentacaoUseCase(IMovimentacaoRepository _movimentacaoRepository)
{
    public void Executar(Guid id)
    {
        var movimentacao = _movimentacaoRepository.ObterPorId(id);
        if (movimentacao != null)
        {
            _movimentacaoRepository.Remover(movimentacao);
        }
    }
}