namespace EnqueteOnline.Domain.Exceptions
{
    public class EnqueteJaPossuiVotosException : DomainException
    {
        public EnqueteJaPossuiVotosException() : base("Não é possível editar a enquete pois já possui votos.")
        {
        }
    }
}
