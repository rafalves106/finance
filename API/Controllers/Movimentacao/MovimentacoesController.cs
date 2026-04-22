using Microsoft.AspNetCore.Mvc;
using Finance.Core.UseCases;
using Finance.Core.Domain;
using Finance.Core.Application.DTOs;

namespace Finance.API.Controllers;

[ApiController]
[Route("api/v1/movimentacoes")]
public class MovimentacoesController(CriarMovimentacaoUseCase criarMovimentacaoUseCase, AtualizarMovimentacaoUseCase atualizarMovimentacaoUseCase, ListarMovimentacoesUseCase listarMovimentacoesUseCase, BuscarMovimentacaoUseCase buscarMovimentacaoUseCase, BuscarEntradaUseCase buscarEntradaUseCase, BuscarSaidaUseCase buscarSaidaUseCase, RemoverMovimentacaoUseCase removerMovimentacaoUseCase, BuscarMovimentacoesPorPeriodoUseCase buscarMovimentacoesPorPeriodoUseCase, BuscarEntradasPorPeriodoUseCase buscarEntradasPorPeriodoUseCase, BuscarSaidasPorPeriodoUseCase buscarSaidasPorPeriodoUseCase) : ControllerBase
{
    [HttpPost]
    public IActionResult CriarMovimentacao([FromBody] MovimentacaoDTO movimentacaoDTO)
    {
        try
        {
            Movimentacao movimentacao = movimentacaoDTO.Tipo switch
            {
                TipoMovimentacao.Entrada => new Entrada(
                    movimentacaoDTO.Titulo,
                    movimentacaoDTO.Descricao,
                    movimentacaoDTO.Valor,
                    movimentacaoDTO.Data,
                    movimentacaoDTO.Fixa,
                    movimentacaoDTO.Periodo
                ),
                TipoMovimentacao.Saida => new Saida(
                    movimentacaoDTO.Titulo,
                    movimentacaoDTO.Descricao,
                    movimentacaoDTO.Valor,
                    movimentacaoDTO.Data,
                    movimentacaoDTO.Fixa,
                    movimentacaoDTO.Periodo
                ),
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

    [HttpGet]
    public IActionResult ListarMovimentacoes()
    {
        try
        {
            var movimentacoes = listarMovimentacoesUseCase.Executar();
            return Ok(movimentacoes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao listar movimentações: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public IActionResult BuscarMovimentacao(Guid id)
    {
        try
        {
            var movimentacao = buscarMovimentacaoUseCase.Executar(id);
            if (movimentacao == null)
            {
                return NotFound($"Nenhuma movimentação encontrada com o ID: {id}");
            }
            return Ok(movimentacao);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar movimentação: {ex.Message}");
        }
    }

    [HttpGet("entradas")]
    public IActionResult BuscarEntradas()
    {
        try
        {
            return Ok(buscarEntradaUseCase.Executar());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar entradas: {ex.Message}");
        }
    }


    [HttpGet("saidas")]
    public IActionResult BuscarSaidas()
    {
        try
        {
            return Ok(buscarSaidaUseCase.Executar());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar saídas: {ex.Message}");
        }
    }

    [HttpGet("periodo")]
    public IActionResult BuscarMovimentacoesPorPeriodo(
[FromQuery] DateTime dataInicio,
[FromQuery] DateTime dataFim,
[FromQuery] TipoMovimentacao? tipo = null)
    {
        try
        {
            var movimentacoes = tipo switch
            {
                TipoMovimentacao.Entrada => buscarEntradasPorPeriodoUseCase.Executar(dataInicio, dataFim),
                TipoMovimentacao.Saida => buscarSaidasPorPeriodoUseCase.Executar(dataInicio, dataFim),
                _ => buscarMovimentacoesPorPeriodoUseCase.Executar(dataInicio, dataFim)
            };

            return Ok(movimentacoes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar movimentações: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarMovimentacao(Guid id, [FromBody] MovimentacaoDTO movimentacaoDTO)
    {
        try
        {
            atualizarMovimentacaoUseCase.Executar(id, movimentacaoDTO);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("{id}")]
    public IActionResult RemoverMovimentacao(Guid id)
    {
        try
        {
            removerMovimentacaoUseCase.Executar(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao remover movimentação: {ex.Message}");
        }
    }
}

