using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Infra.Data.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario, UsuarioId>, IUsuarioRepository
    {
        public UsuarioRepository(EnqueteOnlineDbContext dbContext) : base(dbContext)
        {
        }
    }
}
