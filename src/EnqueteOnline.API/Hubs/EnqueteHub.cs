using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace EnqueteOnline.API.Hubs
{
    
    public class EnqueteHub : Hub
    {
        // Quando o cliente se conecta
        public override async Task OnConnectedAsync()
        {
            // Você pode logar ou rastrear aqui se quiser
            await base.OnConnectedAsync();
        }

        // Quando o cliente se desconecta
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        // Adiciona o cliente ao grupo da enquete
        public async Task EntrarNaEnquete(Guid enqueteId)
        {
            string grupo = ObterGrupo(enqueteId);
            await Groups.AddToGroupAsync(Context.ConnectionId, grupo);
        }

        // Remove o cliente do grupo da enquete
        public async Task SairDaEnquete(Guid enqueteId)
        {
            string grupo = ObterGrupo(enqueteId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, grupo);
        }

        // (opcional) método para envio manual
        public async Task EnviarNotificacaoManual(Guid enqueteId, object payload)
        {
            await Clients.Group(ObterGrupo(enqueteId)).SendAsync("VotoAtualizado", payload);
        }

        private string ObterGrupo(Guid enqueteId) => $"enquete-{enqueteId}";
    }
}
