using Carter;
using EnqueteOnline.Application.Commands.CadastrarVoto;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace EnqueteOnline.API.Endpoints
{
    public class VotoEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/votos");

            group.MapPost("/", [Authorize] async (CadastrarVotoCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.Created($"votos/{result}", result);
            });
        }
    }
}
