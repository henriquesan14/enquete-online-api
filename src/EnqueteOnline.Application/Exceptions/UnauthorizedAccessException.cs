namespace EnqueteOnline.Application.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("Username or password are incorrect")
        {
        }

        public UnauthorizedException(string? message) : base(message)
        {
        }
    }
}
