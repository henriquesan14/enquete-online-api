namespace EnqueteOnline.Application.Contracts.Services.Response
{
    public record AuthTokenResult(string AccessToken, string RefreshToken, DateTime AccessTokenExpiresAt, DateTime RefreshTokenExpiresAt);
}
