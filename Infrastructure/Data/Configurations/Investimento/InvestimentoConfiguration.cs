using Finance.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Data.Configurations;

public class InvestimentoConfiguration : IEntityTypeConfiguration<Investimento>
{
    public void Configure(EntityTypeBuilder<Investimento> builder)
    {
        builder.ToTable("Investimentos");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Nome).IsRequired().HasMaxLength(100);
        builder.Property(i => i.Instituicao).IsRequired().HasMaxLength(100);
        
        builder.Property(i => i.Tipo).HasConversion<string>();
        builder.Property(i => i.TipoRentabilidade).HasConversion<string>();
        builder.Property(i => i.Liquidez).HasConversion<string>();

        builder.Metadata.FindNavigation(nameof(Investimento.Transacoes))!
               .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(i => i.Transacoes)
               .WithOne(t => t.Investimento)
               .HasForeignKey(t => t.InvestimentoId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}