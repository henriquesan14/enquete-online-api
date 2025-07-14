using EnqueteOnline.Domain.Abstractions;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Domain.Entities
{
    public class RefreshToken : Aggregate<RefreshTokenId>
    {
        public string Token { get; set; } = Guid.NewGuid().ToString();
        public UsuarioId UserId { get; private set; } = default!;
        public DateTime ExpiresAt { get; private set; } = default!;
        public DateTime? RevokedAt { get; private set; } = default!;
        public string CreatedByIp { get; private set; } = default!;
        public string? ReplacedByToken { get; private set; }
        public string? RevokedByIp { get; private set; }

        public bool IsExpired => DateTime.Now >= ExpiresAt;
        public bool IsRevoked => RevokedAt.HasValue;


        public virtual Usuario User { get; private set; } = default!;

        public static RefreshToken Create(RefreshTokenId id, string token, UsuarioId userId, string createdByIp, DateTime expiresAt)
        {
            return new RefreshToken
            {
                Id = id,
                Token = token,
                UserId = userId,
                ExpiresAt = expiresAt,
                CreatedByIp = createdByIp,
                RevokedAt = null,
            };
        }

        public void Revoke(string replacedByIp)
        {
            RevokedAt = DateTime.Now;
            RevokedByIp = replacedByIp;
        }

        public void SetReplacedByToken(string replacedByToken)
        {
            ReplacedByToken = replacedByToken;
        }
    }
}
