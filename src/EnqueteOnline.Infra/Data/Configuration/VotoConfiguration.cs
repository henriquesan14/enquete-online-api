using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnqueteOnline.Infra.Data.Configuration
{
    public class VotoConfiguration : IEntityTypeConfiguration<Voto>
    {
        public void Configure(EntityTypeBuilder<Voto> builder)
        {
  
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id)
                .HasConversion(id => id.Value, value => VotoId.Of(value));

            builder.Property(v => v.Ip)
                .IsRequired()
                .HasMaxLength(45);

            builder.HasOne(v => v.Enquete)
                .WithMany(e => e.Votos)
                .HasForeignKey(v => v.EnqueteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.Opcao)
                .WithMany()
                .HasForeignKey(v => v.OpcaoEnqueteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
