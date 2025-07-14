namespace EnqueteOnline.Application.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException(string message = "Você não tem permissão para executar esta ação.")
            : base(message) { }
    }
}
