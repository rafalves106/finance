using Finance.Core.Domain;

namespace Finance.Core.Application.DTOs;
public record MovimentacaoDTO(string Titulo, string Descricao, decimal Valor, DateTime Data, TipoMovimentacao Tipo, bool Fixa = false, int? DiaFixo = null, int Periodo = 0);