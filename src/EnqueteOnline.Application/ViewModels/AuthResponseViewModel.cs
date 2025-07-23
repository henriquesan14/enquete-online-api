namespace EnqueteOnline.Application.ViewModels
{
    public record AuthResponseViewModel(UsuarioViewModel User, string AccessToken, string? RefreshToken);
}
