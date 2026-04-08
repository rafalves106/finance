using Finance.Core.Repositories;
using Finance.Core.Application.DTOs;

namespace Finance.Core.UseCases;

public class AtualizarMovimentacaoUseCase(IMovimentacaoRepository _movimentacaoRepository)
{
    public void Executar(Guid id, MovimentacaoDTO dto)
    {
        var movimentacaoExistente = _movimentacaoRepository.ObterPorId(id);

        if (movimentacaoExistente == null)
        {
            throw new Exception("Movimentação não encontrada.");
        }

        movimentacaoExistente.AtualizarDados(dto.Titulo, dto.Descricao, dto.Valor, dto.Data, dto.Fixa, dto.DiaFixo, dto.Periodo);

        _movimentacaoRepository.Atualizar(movimentacaoExistente);
    }
}