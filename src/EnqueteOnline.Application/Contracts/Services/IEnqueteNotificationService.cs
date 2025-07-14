namespace EnqueteOnline.Application.Contracts.Services
{
    public interface IEnqueteNotificationService
    {
        Task NotificarVotoAtualizadoAsync(Guid enqueteId);
    }
}
