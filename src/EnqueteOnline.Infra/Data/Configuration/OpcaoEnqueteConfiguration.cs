using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnqueteOnline.Infra.Data.Configuration
{
    public class OpcaoEnqueteConfiguration : IEntityTypeConfiguration<OpcaoEnquete>
    {
        public void Configure(EntityTypeBuilder<OpcaoEnquete> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .HasConversion(id => id.Value, value => OpcaoEnqueteId.Of(value));

            builder.Property(o => o.Descricao)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
