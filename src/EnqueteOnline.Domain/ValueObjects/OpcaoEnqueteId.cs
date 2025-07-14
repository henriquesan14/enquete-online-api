using EnqueteOnline.Domain.Exceptions;

namespace EnqueteOnline.Domain.ValueObjects
{
    public record OpcaoEnqueteId
    {
        public Guid Value { get; }
        private OpcaoEnqueteId(Guid value) => Value = value;
        public static OpcaoEnqueteId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("OpcaoEnqueteId cannot be empty.");
            }
            return new OpcaoEnqueteId(value);
        }
    }
}
