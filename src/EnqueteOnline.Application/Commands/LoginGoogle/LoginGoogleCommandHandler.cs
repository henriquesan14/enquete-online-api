﻿using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Application.Extensions;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Application.Abstractions;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text;
using System.Text.Json;

namespace EnqueteOnline.Application.Commands.LoginGoogle
{
    public class LoginGoogleCommandHandler(IGoogleAuthService googleAuthService, IUnitOfWork unitOfWork, ITokenService tokenService,
        IConfiguration configuration, ICurrentUserService currentUserService) : ICommandHandler<LoginGoogleCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(LoginGoogleCommand request, CancellationToken cancellationToken)
        {
            var token = await googleAuthService.ObterTokenAsync(request.code);
            if (token is null) return Result<string>.Failure("Erro ao obter token do Google.", HttpStatusCode.Unauthorized);

            var userGoogle = await googleAuthService.ObterUsuarioAsync(token.AccessToken);
            if (userGoogle is null) return Result<string>.Failure("Erro ao obter token do Google.", HttpStatusCode.Unauthorized);

            
            var user = await unitOfWork.Usuarios.GetSingleAsync(u => u.GoogleId == userGoogle.Sub);

            if (user is null)
            {
                user = Usuario.CreateGoogle(
                    nome: userGoogle.Name,
                    email: Email.Of(userGoogle.Email),
                    avatarUrl: userGoogle.Picture,
                    googleId: userGoogle.Sub
                );
                await unitOfWork.Usuarios.AddAsync(user);
            }

            var authToken = tokenService.GenerateAccessToken(user);

            var refreshToken = RefreshToken.Create(
                id: RefreshTokenId.Of(Guid.NewGuid()),
                token: authToken.RefreshToken,
                userId: UsuarioId.Of(user.Id.Value),
                expiresAt: authToken.RefreshTokenExpiresAt,
                createdByIp: currentUserService.IpAddress!
                );
            await unitOfWork.RefreshTokens.AddAsync(refreshToken);
            await unitOfWork.CompleteAsync();

            var authResponse = new AuthResponseViewModel
            (
                User: user.ToViewModel(),
                authToken.AccessToken,
                null
            );

            currentUserService.SetRefreshTokenCookies(refreshToken.Token);

            var redirectUrl = GenerateRedirectUrl(authResponse);

            return Result<string>.Success(redirectUrl);
        }
        private string GenerateRedirectUrl(AuthResponseViewModel authResponse)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(authResponse, options);

            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            return $"{configuration["Google:RedirectAppUrl"]}#data={base64}";
        }
    }
}
