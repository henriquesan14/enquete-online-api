using EnqueteOnline.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using FluentValidation;
using EnqueteOnline.Domain.Exceptions;

namespace EnqueteOnline.API.ErrorHandling
{
    public class CustomExceptionHandler
    (ILogger<CustomExceptionHandler> logger)
    : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(
                "Error Message: {exceptionMessage}, Time of occurrence {time}",
                exception.Message, DateTime.Now);

            (string Detail, string Title, int StatusCode) details = exception switch
            {
                InternalServerException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError
                ),
                ValidationException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                DomainException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                DbUpdateException dbEx when dbEx.InnerException is PostgresException pgEx && pgEx.SqlState == "23503" => (
                    "Não foi possível concluir a operação porque o registro está associado a outro recurso no sistema.",
                    "Violação de Integridade Referencial",
                    StatusCodes.Status409Conflict
                ),
                _ =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
            };

            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = context.Request.Path
            };

            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

            if (exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
            }

            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
            return true;
        }
    }
}
