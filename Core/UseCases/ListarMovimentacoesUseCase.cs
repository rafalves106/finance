using Finance.Core.Domain;
using Finance.Core.Repositories;

namespace Finance.Core.UseCases;

public class ListarMovimentacoesUseCase(IMovimentacaoRepository _movimentacaoRepository)
{
    public IEnumerable<Movimentacao> Executar()
    {
        return _movimentacaoRepository.Listar();
    }
}