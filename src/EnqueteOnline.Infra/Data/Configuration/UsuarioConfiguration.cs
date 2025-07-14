using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnqueteOnline.Infra.Data.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasConversion(
                    id => id.Value,
                    value => UsuarioId.Of(value)
                );

            builder.Property(d => d.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Email)
                .HasConversion(
                    email => email.Value,
                    email => Email.Of(email)
                 )
                .IsRequired()
                .HasMaxLength(200);
            
            builder
                .HasIndex(e => e.Email)
                .IsUnique();

            builder.HasIndex(u => u.GoogleId).IsUnique();
            builder.HasIndex(u => u.FacebookId).IsUnique();

            builder.Property(d => d.AvatarUrl)
                .HasMaxLength(255);
        }
    }
}
