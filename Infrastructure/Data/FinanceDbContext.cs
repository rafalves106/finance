using Microsoft.EntityFrameworkCore;
using Finance.Core.Domain;
using Finance.Infrastructure.Data.Configurations;

namespace Finance.Infrastructure.Data;

public class FinanceDbContext : DbContext
{
    public DbSet<Movimentacao> Movimentacoes { get; set; }
    public DbSet<Entrada> Entradas { get; set; }
    public DbSet<Saida> Saidas { get; set; }
    public DbSet<Investimento> Investimentos { get; set; }
    public DbSet<TransacaoInvestimento> TransacoesInvestimento { get; set; }

    public DbSet<Meta> Metas { get; set; }

    public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // --- Movimentacao (existente) ---
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
            entity.Property(e => e.Descricao).HasMaxLength(500).IsRequired(false);
            entity.Property(e => e.Valor).HasPrecision(18, 2);

            entity.HasOne<Investimento>()
                  .WithMany()
                  .HasForeignKey(m => m.InvestimentoId)
                  .IsRequired(false)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(e => e.Data);
            entity.HasIndex(e => e.Tipo);
            entity.HasIndex(e => e.InvestimentoId);
        });

        // ← TransacaoInvestimento como entidade normal (tem DbSet próprio)
        modelBuilder.Entity<TransacaoInvestimento>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Tipo).HasConversion<string>();
            entity.Property(e => e.Valor).HasPrecision(18, 2);
        });

        // --- Meta (novo) ---
        modelBuilder.Entity<Meta>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Valor).HasPrecision(18, 2);
        });

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new InvestimentoConfiguration());
    }
}