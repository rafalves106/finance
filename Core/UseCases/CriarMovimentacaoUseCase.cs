using Finance.Core.Domain;
using Finance.Core.Repositories;

namespace Finance.Core.UseCases;

public class CriarMovimentacaoUseCase(IMovimentacaoRepository _movimentacaoRepository)
{
    public void Executar(Movimentacao movimentacao)
    {
        _movimentacaoRepository.Adicionar(movimentacao);
    }
}