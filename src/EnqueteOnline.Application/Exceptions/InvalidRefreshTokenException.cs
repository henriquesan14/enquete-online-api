namespace EnqueteOnline.Application.Exceptions
{
    public class InvalidRefreshTokenException : UnauthorizedException
    {
        public InvalidRefreshTokenException(string? message) : base(message)
        {
        }
    }
}
