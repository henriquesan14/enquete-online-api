using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Application.Contracts.Data
{
    public interface IUsuarioRepository : IAsyncRepository<Usuario, UsuarioId>
    {
    }
}
