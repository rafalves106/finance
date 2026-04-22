using Finance.Core.Domain;
using Finance.Core.Repositories;
using Finance.Core.Application.DTOs;

namespace Finance.Core.UseCases;

public class CriarMetaUseCase(IMetaRepository _metaRepository)
{
  public Guid Executar(MetaDTO dto)
  {
    var meta = new Meta(dto.Descricao, dto.Valor, dto.DataAlvo);
    return _metaRepository.Adicionar(meta);
  }
}