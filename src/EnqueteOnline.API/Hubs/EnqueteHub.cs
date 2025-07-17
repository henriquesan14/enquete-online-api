using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace EnqueteOnline.API.Hubs
{
    [Authorize]
    public class EnqueteHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var user = Context.User?.Identity?.Name;
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task EntrarNaEnquete(Guid enqueteId)
        {
            string grupo = ObterGrupo(enqueteId);
            await Groups.AddToGroupAsync(Context.ConnectionId, grupo);
        }

        public async Task SairDaEnquete(Guid enqueteId)
        {
            string grupo = ObterGrupo(enqueteId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, grupo);
        }

        public async Task EnviarNotificacaoManual(Guid enqueteId, object payload)
        {
            await Clients.Group(ObterGrupo(enqueteId)).SendAsync("VotoAtualizado", payload);
        }

        private string ObterGrupo(Guid enqueteId) => $"enquete-{enqueteId}";
    }
}
