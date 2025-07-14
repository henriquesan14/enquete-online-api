using EnqueteOnline.Domain.Exceptions;

namespace EnqueteOnline.Domain.ValueObjects
{
    public record VotoId
    {
        public Guid Value { get; }
        private VotoId(Guid value) => Value = value;
        public static VotoId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("VotoId cannot be empty.");
            }
            return new VotoId(value);
        }
    }
}
