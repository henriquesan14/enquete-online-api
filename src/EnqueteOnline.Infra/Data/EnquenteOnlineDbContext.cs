using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EnqueteOnline.Infra.Data
{
    public class EnqueteOnlineDbContext : DbContext, IEnqueteOnlineDbContext
    {
        public EnqueteOnlineDbContext(DbContextOptions<EnqueteOnlineDbContext> options)
        : base(options) { }
        public DbSet<Enquete> Enquetes => Set<Enquete>();

        public DbSet<OpcaoEnquete> OpcoesEnquete => Set<OpcaoEnquete>();

        public DbSet<Voto> Votos => Set<Voto>();

        public DbSet<Usuario> Usuarios => Set<Usuario>();

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetColumnType("timestamp");
                    }
                }
            }

            foreach (var ownedType in builder.Model.GetEntityTypes().Where(t => t.IsOwned()))
            {
                foreach (var property in ownedType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetColumnType("timestamp");
                    }
                }
            }

            base.OnModelCreating(builder);
        }
    }
}
