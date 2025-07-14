using EnqueteOnline.Domain.Exceptions;

namespace EnqueteOnline.Domain.ValueObjects
{
    public record Email
    {
        public string Value { get; }
        private Email(string value) => Value = value;
        public static Email Of(string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (string.IsNullOrEmpty(value))
            {
                throw new DomainException("Email cannot be empty.");
            }
            return new Email(value);
        }
    }
}
