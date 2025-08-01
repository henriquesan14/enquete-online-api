﻿using Carter;
using EnqueteOnline.API.Extensions;
using EnqueteOnline.Application.Commands.AtualizarEnquete;
using EnqueteOnline.Application.Commands.CadastrarEnquete;
using EnqueteOnline.Application.Commands.ExcluirEnquete;
using EnqueteOnline.Application.Pagination;
using EnqueteOnline.Application.Queries.BuscarEnquetePorId;
using EnqueteOnline.Application.Queries.ListarEnquetes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnqueteOnline.API.Endpoints
{
    public class EnqueteEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/enquetes");

            group.MapPost("/", [Authorize] async (CadastrarEnqueteCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return result.ToMinimalApiResult(location: $"enquetes/{result.Data.Id}");
            });

            group.MapGet("/", [Authorize] async ([AsParameters] PaginationRequest request,  ISender sender, [FromQuery] string? titulo = null) =>
            {
                var query = new ListarEnquetesQuery(request.PageNumber, request.PageSize, titulo);
                var result = await sender.Send(query);

                return result.ToMinimalApiResult();
            });

            group.MapGet("/{id}", [Authorize] async (Guid id, ISender sender) =>
            {
                var query = new BuscarEnquetePorIdQuery(id);
                var result = await sender.Send(query);

                return result.ToMinimalApiResult();
            });

            group.MapDelete("/{id}", [Authorize] async (Guid id, ISender sender) =>
            {
                var query = new ExcluirEnqueteCommand(id);
                var result = await sender.Send(query);

                return result.ToMinimalApiResult();
            });

            group.MapPut("/", [Authorize] async (AtualizarEnqueteCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return result.ToMinimalApiResult();
            });
        }
    }
}
