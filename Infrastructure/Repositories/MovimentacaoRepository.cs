using Finance.Core.Domain;
using Finance.Core.Repositories;
using Finance.Infrastructure.Data;

namespace Finance.Infrastructure.Repositories;

public class MovimentacaoRepository : IMovimentacaoRepository
{
    private readonly MovimentacaoDbContext _context;

    public MovimentacaoRepository(MovimentacaoDbContext context)
    {
        _context = context;
    }

    public void Adicionar(Movimentacao movimentacao)
    {
        _context.Movimentacoes.Add(movimentacao);
        _context.SaveChanges();
    }

    public IEnumerable<Movimentacao> Listar()
    {
        return _context.Movimentacoes.ToList();
    }

    public void Remover(Movimentacao movimentacao)
    {
        _context.Movimentacoes.Remove(movimentacao);
        _context.SaveChanges();
    }

    public void Atualizar(Guid id,Movimentacao movimentacao)
    {
        _context.Movimentacoes.Update(movimentacao);
        _context.SaveChanges();
    }

    public Movimentacao? ObterPorId(Guid id)
    {
        return _context.Movimentacoes.Find(id);
    }

    public IEnumerable<Entrada> ListarEntradas()
    {
        return _context.Movimentacoes.OfType<Entrada>().ToList();
    }

    public IEnumerable<Saida> ListarSaidas()
    {
        return _context.Movimentacoes.OfType<Saida>().ToList();
    }
}