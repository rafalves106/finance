using Microsoft.AspNetCore.Mvc;
using Finance.Core.UseCases;
using Finance.Core.Domain;

namespace Finance.API.Controllers;

    public record MovimentacaoDTO(TipoMovimentacao Tipo, string Titulo, string Descricao, decimal Valor, DateTime Data);

[ApiController]
[Route("api/v1/movimentacoes")]
public class MovimentacoesController(CriarMovimentacaoUseCase criarMovimentacaoUseCase, AtualizarMovimentacaoUseCase atualizarMovimentacaoUseCase) : ControllerBase
{
    [HttpPost]
    public IActionResult CriarMovimentacao([FromBody] MovimentacaoDTO movimentacaoDTO)
    {
        try
        {
            Movimentacao movimentacao = movimentacaoDTO.Tipo switch
            {
                TipoMovimentacao.Entrada => new Entrada(movimentacaoDTO.Titulo, movimentacaoDTO.Descricao, movimentacaoDTO.Valor, movimentacaoDTO.Data),
                TipoMovimentacao.Saida => new Saida(movimentacaoDTO.Titulo, movimentacaoDTO.Descricao, movimentacaoDTO.Valor, movimentacaoDTO.Data),
                _ => throw new ArgumentException("Tipo de movimentação inválido.")
            };

            criarMovimentacaoUseCase.Executar(movimentacao);
            return CreatedAtAction(nameof(CriarMovimentacao), new { id = movimentacao.Id }, movimentacao);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarMovimentacao(Guid id, [FromBody] MovimentacaoDTO movimentacaoDTO)
    {
        try
        {
            Movimentacao movimentacao = movimentacaoDTO.Tipo switch
            {
                TipoMovimentacao.Entrada => new Entrada(movimentacaoDTO.Titulo, movimentacaoDTO.Descricao, movimentacaoDTO.Valor, movimentacaoDTO.Data),
                TipoMovimentacao.Saida => new Saida(movimentacaoDTO.Titulo, movimentacaoDTO.Descricao, movimentacaoDTO.Valor, movimentacaoDTO.Data),
                _ => throw new ArgumentException("Tipo de movimentação inválido.")
            };

            atualizarMovimentacaoUseCase.Executar(id, movimentacao);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

