using EnqueteOnline.Domain.Exceptions;

namespace EnqueteOnline.Domain.ValueObjects
{
    public record EnqueteId
    {
        public Guid Value { get; }
        private EnqueteId(Guid value) => Value = value;
        public static EnqueteId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("EnqueteId cannot be empty.");
            }
            return new EnqueteId(value);
        }
    }
}
