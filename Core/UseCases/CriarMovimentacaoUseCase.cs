using Finance.Core.Domain;
using Finance.Core.Repositories;

namespace Finance.Core.UseCases;


public class CriarMovimentacaoUseCase(IMovimentacaoRepository _movimentacaoRepository)
{
    public Guid Executar(Movimentacao movimentacao)
    {
        if (movimentacao.Fixa)
        {
            int diaFixo = movimentacao.DiaFixo.Value; 
            var grupoRecorrenciaId = Guid.NewGuid();

            int diasNoMes = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var dataBase = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Math.Min(diasNoMes, diaFixo));

            for (int i = 0; i < movimentacao.Periodo; i++)
            {
                var dataDaParcela = dataBase.AddMonths(i);
        
                int diasNoMesDaParcela = DateTime.DaysInMonth(dataDaParcela.Year, dataDaParcela.Month);
                dataDaParcela = new DateTime(dataDaParcela.Year, dataDaParcela.Month, Math.Min(diasNoMesDaParcela, diaFixo));

                Movimentacao novaOcorrencia = movimentacao.ClonarComNovaData(dataDaParcela, grupoRecorrenciaId);

                _movimentacaoRepository.Adicionar(novaOcorrencia);
            }
            return grupoRecorrenciaId; 
        }

        return _movimentacaoRepository.Adicionar(movimentacao);
    }
}