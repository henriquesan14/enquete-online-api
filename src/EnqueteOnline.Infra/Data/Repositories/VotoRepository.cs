using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Infra.Data.Repositories
{
    public class VotoRepository : RepositoryBase<Voto, VotoId>, IVotoRepository
    {
        public VotoRepository(EnqueteOnlineDbContext dbContext) : base(dbContext)
        {
        }
    }
}
