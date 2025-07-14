using Carter;
using EnqueteOnline.Application.Commands.CadastrarUsuario;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace EnqueteOnline.API.Endpoints
{
    public class UserEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/usuarios");

            group.MapPost("/", [Authorize] async (CadastrarUsuarioCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.Created($"usuarios/{result}", result);
            });
        }
    }
}
