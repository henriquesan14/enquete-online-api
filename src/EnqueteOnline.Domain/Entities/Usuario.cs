using EnqueteOnline.Domain.Abstractions;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Domain.Entities
{
    public class Usuario : Aggregate<UsuarioId>
    {
        public string Nome { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public string? AvatarUrl { get; private set; } = default!;

        public string? GoogleId { get; private set; } = default!;
        public string? FacebookId { get; private set; } = default!;

        public static Usuario Create(string nome, Email email, string avatarUrl)
        {
            return new Usuario
            {
                Id = UsuarioId.Of(Guid.NewGuid()),
                Nome = nome,
                Email = email,
                AvatarUrl = avatarUrl
            };
        }

        public static Usuario CreateGoogle(string nome, Email email, string avatarUrl, string googleId)
        {
            return new Usuario
            {
                Id = UsuarioId.Of(Guid.NewGuid()),
                Nome = nome,
                Email = email,
                AvatarUrl = avatarUrl,
                GoogleId = googleId
            };
        }

        public static Usuario CreateFacebook(string nome, Email email, string avatarUrl, string facebookId)
        {
            return new Usuario
            {
                Id = UsuarioId.Of(Guid.NewGuid()),
                Nome = nome,
                Email = email,
                AvatarUrl = avatarUrl,
                FacebookId = facebookId
            };
        }
        public ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

    }
}
