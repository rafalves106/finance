using Finance.Core.Domain;
using Finance.Core.Repositories;

namespace Finance.Core.UseCases;

public class AtualizarMovimentacaoUseCase(IMovimentacaoRepository _movimentacaoRepository)
{
    public void Executar(Guid id, Movimentacao movimentacao)
    {
        _movimentacaoRepository.Atualizar(id,movimentacao);
    }
}