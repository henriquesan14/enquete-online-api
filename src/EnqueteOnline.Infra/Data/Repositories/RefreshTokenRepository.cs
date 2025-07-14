using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace EnqueteOnline.Infra.Data.Repositories
{
    public class RefreshTokenRepository : RepositoryBase<RefreshToken, RefreshTokenId>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(EnqueteOnlineDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteRange(List<RefreshTokenId> RefreshTokenIds)
        {
            await DbContext.RefreshTokens
            .Where(p => RefreshTokenIds.Contains(p.Id))
            .ExecuteDeleteAsync();
        }
    }
}
