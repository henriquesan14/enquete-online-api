namespace EnqueteOnline.Application.Contracts.Services
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? IpAddress { get; }
        string? RefreshToken { get; }
        void SetCookieTokens(string accessToken, string refreshToken);
        void RemoveCookiesToken();
    }
}
