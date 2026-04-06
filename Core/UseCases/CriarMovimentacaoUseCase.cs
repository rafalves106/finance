using Finance.Core.Domain;
using Finance.Core.Repositories;

namespace Finance.Core.UseCases;

public class CriarMovimentacaoUseCase(IMovimentacaoRepository _movimentacaoRepository)
{
    public Guid Executar(Movimentacao movimentacao)
    {
        return _movimentacaoRepository.Adicionar(movimentacao);
    }
}