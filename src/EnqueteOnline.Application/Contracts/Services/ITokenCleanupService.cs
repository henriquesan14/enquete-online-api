namespace EnqueteOnline.Application.Contracts.Services
{
    public interface ITokenCleanupService
    {
        Task CleanupExpiredAndRevokedTokensAsync();
    }
}
