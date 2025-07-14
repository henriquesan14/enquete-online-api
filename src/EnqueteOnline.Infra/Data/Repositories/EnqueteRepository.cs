using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Infra.Data.Repositories
{
    public class EnqueteRepository : RepositoryBase<Enquete, EnqueteId>, IEnqueteRepository
    {
        public EnqueteRepository(EnqueteOnlineDbContext dbContext) : base(dbContext)
        {
        }
    }
}
