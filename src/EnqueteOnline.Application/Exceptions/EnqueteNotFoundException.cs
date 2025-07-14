namespace EnqueteOnline.Application.Exceptions
{
    public class EnqueteNotFoundException : NotFoundException
    {

        public EnqueteNotFoundException(Guid id) : base("Enquete", id)
        {
        }
    }
}
