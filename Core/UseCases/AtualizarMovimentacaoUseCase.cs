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

        movimentacaoExistente.Titulo = dto.Titulo;
        movimentacaoExistente.Descricao = dto.Descricao;
        movimentacaoExistente.Valor = dto.Valor;
        movimentacaoExistente.Data = dto.Data;

        _movimentacaoRepository.Atualizar(movimentacaoExistente);
    }
}