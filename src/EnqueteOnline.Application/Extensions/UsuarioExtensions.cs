using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Domain.Entities;

namespace EnqueteOnline.Application.Extensions
{
    public static class UsuarioExtensions
    {
        public static IEnumerable<UsuarioViewModel> ToViewModelList(this IEnumerable<Usuario> usuarios)
        {
            return usuarios.Select(usuario => EntityToViewModel(usuario!));
        }

        public static UsuarioViewModel ToViewModel(this Usuario enquete)
        {
            return EntityToViewModel(enquete);
        }

        private static UsuarioViewModel EntityToViewModel(Usuario usuario)
        {
            return new UsuarioViewModel
            (
                Id: usuario.Id.Value,
                Nome: usuario.Nome,
                Email: usuario.Email.Value,
                AvatarUrl: usuario.AvatarUrl
            );
        }
    }
}
