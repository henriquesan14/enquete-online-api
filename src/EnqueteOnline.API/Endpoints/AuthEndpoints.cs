using Carter;
using EnqueteOnline.Application.Commands.LoginAccessTokenGoogle;
using EnqueteOnline.Application.Commands.LoginFacebook;
using EnqueteOnline.Application.Commands.LoginGoogle;
using EnqueteOnline.Application.Commands.RenewRefreshToken;
using EnqueteOnline.Application.Commands.RevokeRefreshToken;
using MediatR;

namespace EnqueteOnline.API.Endpoints
{
    public class AuthEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth");

            group.MapGet("/google/callback", async (string code, ISender sender) =>
            {
                LoginGoogleCommand command = new LoginGoogleCommand(code);
                var result = await sender.Send(command);

                return Results.Redirect(result);
            });

            group.MapGet("/facebook/callback", async (string code, ISender sender) =>
            {
                LoginFacebookCommand command = new LoginFacebookCommand(code);
                var result = await sender.Send(command);

                return Results.Redirect(result);
            });

            group.MapPost("/refresh-token", async (RefreshTokenCommand command, ISender sender, HttpResponse response) =>
            {
                var result = await sender.Send(command);

                return Results.Ok(result);
            });

            group.MapPost("/logout", async (RevokeRefreshTokenCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.NoContent();
            });

            group.MapPost("/login/google", async (LoginAccessTokenGoogleCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.Ok(result.User);
            });
        }
    }
}
