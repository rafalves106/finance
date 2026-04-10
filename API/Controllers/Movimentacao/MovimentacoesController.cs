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
            if (movimentacoes == null || !movimentacoes.Any())
            {
                return NotFound("Nenhuma movimentação encontrada.");
            }
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
            var entradas = buscarEntradaUseCase.Executar();
            if (entradas == null || !entradas.Any())
            {
                return NotFound("Nenhuma entrada encontrada.");
            }
            return Ok(entradas);
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
            var saidas = buscarSaidaUseCase.Executar();
            if (saidas == null || !saidas.Any())
            {
                return NotFound("Nenhuma saída encontrada.");
            }
            return Ok(saidas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar saídas: {ex.Message}");
        }
    }

    [HttpGet("periodo")]
    public IActionResult BuscarMovimentacoesPorPeriodo([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim, [FromQuery] TipoMovimentacao tipo)
    {
        try
        {
            var movimentacoes = buscarMovimentacoesPorPeriodoUseCase.Executar(dataInicio, dataFim);

            if (tipo == TipoMovimentacao.Entrada)
            {
                movimentacoes = buscarEntradasPorPeriodoUseCase.Executar(dataInicio, dataFim);
            }
            else if (tipo == TipoMovimentacao.Saida)
            {
                movimentacoes = buscarSaidasPorPeriodoUseCase.Executar(dataInicio, dataFim);
            }

            if (movimentacoes == null || !movimentacoes.Any())
            {
                return NotFound("Nenhuma movimentação encontrada para o período especificado.");
            }
            return Ok(movimentacoes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar movimentações por período: {ex.Message}");
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

