using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnqueteOnline.Infra.Data.Configuration
{
    public class EnqueteConfiguration : IEntityTypeConfiguration<Enquete>
    {
        public void Configure(EntityTypeBuilder<Enquete> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasConversion(
                    id => id.Value,
                    value => EnqueteId.Of(value)
                );

            builder.Property(e => e.Titulo)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(d => d.Descricao)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(d => d.Encerramento)
                .IsRequired();

            builder.HasMany(e => e.Opcoes)
                .WithOne(o => o.Enquete)
                .HasForeignKey(o => o.EnqueteId);

            builder.HasMany(e => e.Votos)
                .WithOne(o => o.Enquete)
                .HasForeignKey(o => o.EnqueteId);
        }
    }
}
