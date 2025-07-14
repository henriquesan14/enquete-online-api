using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Application.Contracts.Data
{
    public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken, RefreshTokenId>
    {
        Task DeleteRange(List<RefreshTokenId> RefreshTokenIds);
    }
}
