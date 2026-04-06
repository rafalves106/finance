using Microsoft.EntityFrameworkCore;
using Finance.Core.Domain;

namespace Finance.Infrastructure.Data;

public class MovimentacaoDbContext : DbContext
{
    public DbSet<Movimentacao> Movimentacoes { get; set; }
    public DbSet<Entrada> Entradas { get; set; }
    public DbSet<Saida> Saidas { get; set; }

    public MovimentacaoDbContext(DbContextOptions<MovimentacaoDbContext> options) : base(options)
    {
    }

protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movimentacao>()
            .HasDiscriminator<TipoMovimentacao>("Tipo")
            .HasValue<Entrada>(TipoMovimentacao.Entrada)
            .HasValue<Saida>(TipoMovimentacao.Saida);
            
        modelBuilder.Entity<Movimentacao>()
            .Property(e => e.Tipo)
            .HasConversion<string>();

        modelBuilder.Entity<Movimentacao>(entity => 
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Descricao).HasMaxLength(500);
            entity.Property(e => e.Valor).HasPrecision(18, 2);
        });

        base.OnModelCreating(modelBuilder);
    }
}