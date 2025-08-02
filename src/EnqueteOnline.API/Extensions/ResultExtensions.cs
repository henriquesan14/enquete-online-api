using EnqueteOnline.Application.Abstractions;
using System.Net;

namespace EnqueteOnline.API.Extensions
{
    public static class ResultExtensions
    {
        public static IResult ToMinimalApiResult(this Result result, string? location = null) =>
            result switch
            {
                { IsSuccess: true } when result.GetType().IsGenericType && result.GetType().GetGenericTypeDefinition() == typeof(Result<>) =>
                    result.StatusCode == HttpStatusCode.Created
                        ? Results.Created(location, result.GetType().GetProperty("Data")?.GetValue(result)) // 201 com location (ou null)
                        : Results.Json(result.GetType().GetProperty("Data")?.GetValue(result), statusCode: (int)result.StatusCode),

                { IsSuccess: true } => Results.StatusCode((int)result.StatusCode),

                _ => Results.Problem(detail: result.Message, statusCode: (int)result.StatusCode)
            };
    }
}
