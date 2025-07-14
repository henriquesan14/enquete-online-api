namespace EnqueteOnline.Application.Exceptions
{
    public class RefreshTokenNotFoundException : NotFoundException
    {
        public RefreshTokenNotFoundException(object key) : base("RefreshToken", key)
        {
        }
    }
}
