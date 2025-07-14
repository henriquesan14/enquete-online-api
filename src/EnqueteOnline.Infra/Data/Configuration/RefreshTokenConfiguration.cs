using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnqueteOnline.Infra.Data.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Id)
                .HasConversion(
                    id => id.Value,
                    value => RefreshTokenId.Of(value))
                .ValueGeneratedNever();

            builder.Property(rt => rt.Token)
                .IsRequired();

            builder.Property(rt => rt.UserId)
                .IsRequired();

            builder.Property(rt => rt.ExpiresAt)
                .IsRequired();

            builder.Property(rt => rt.RevokedAt)
                .IsRequired(false);

            builder.Property(rt => rt.CreatedByIp)
                .IsRequired();

            builder.Property(rt => rt.ReplacedByToken)
                .IsRequired(false);

            builder.Property(rt => rt.RevokedByIp)
                .IsRequired(false);

            builder.HasIndex(rt => rt.Token).IsUnique();

            builder.HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
