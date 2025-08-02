using System.Net;

namespace EnqueteOnline.Application.Abstractions
{
    public class Result
    {
        public bool IsSuccess { get; init; }
        public string Message { get; init; }
        public HttpStatusCode StatusCode { get; init; }

        protected Result(bool isSuccess, string message, HttpStatusCode statusCode)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = statusCode;
        }

        public static Result Success(string message = "Sucesso", HttpStatusCode statusCode = HttpStatusCode.OK)
            => new(true, message, statusCode);

        public static Result Failure(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new(false, message, statusCode);
    }
}
