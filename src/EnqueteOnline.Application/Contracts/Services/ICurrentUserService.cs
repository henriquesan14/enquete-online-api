namespace EnqueteOnline.Application.Contracts.Services
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? IpAddress { get; }
        string? RefreshToken { get; }
        void SetRefreshTokenCookies(string refreshToken);
        void RemoveRefreshTokenCookies();
        bool IsMobileClient();
    }
}
