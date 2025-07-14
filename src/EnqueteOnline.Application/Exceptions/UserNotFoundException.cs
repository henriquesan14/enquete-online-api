namespace EnqueteOnline.Application.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(Guid id) : base("User", id)
        {
        }
    }
}
