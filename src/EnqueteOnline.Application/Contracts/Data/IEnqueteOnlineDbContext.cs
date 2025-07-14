using EnqueteOnline.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnqueteOnline.Application.Contracts.Data
{
    public interface IEnqueteOnlineDbContext
    {
        DbSet<Enquete> Enquetes { get; }
        DbSet<OpcaoEnquete> OpcoesEnquete { get; }
        DbSet<Voto> Votos { get; }

        DbSet<Usuario> Usuarios { get; }

        DbSet<RefreshToken> RefreshTokens { get; }
    }
}
