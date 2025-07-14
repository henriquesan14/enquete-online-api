using EnqueteOnline.API.Hubs;
using EnqueteOnline.Application.Contracts.Services;
using Microsoft.AspNetCore.SignalR;

namespace EnqueteOnline.API.Notification
{
    public class EnqueteNotificationService : IEnqueteNotificationService
    {
        private readonly IHubContext<EnqueteHub> _hubContext;

        public EnqueteNotificationService(IHubContext<EnqueteHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotificarVotoAtualizadoAsync(Guid enqueteId)
        {
            await _hubContext.Clients
                .Group($"enquete-{enqueteId}")
                .SendAsync("VotoAtualizado", enqueteId);
        }
    }
}
