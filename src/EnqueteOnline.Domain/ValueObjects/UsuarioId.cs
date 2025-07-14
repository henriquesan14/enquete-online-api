using EnqueteOnline.Domain.Exceptions;

namespace EnqueteOnline.Domain.ValueObjects
{
    public record UsuarioId
    {
        public Guid Value { get; }
        private UsuarioId(Guid value) => Value = value;
        public static UsuarioId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("UsuarioId cannot be empty.");
            }
            return new UsuarioId(value);
        }
    }
}
