using System.Net;

namespace EnqueteOnline.Application.Abstractions
{
    public class Result<T> : Result
    {
        public T Data { get; init; }

        private Result(T data, string message, HttpStatusCode statusCode)
            : base(true, message, statusCode)
        {
            Data = data;
        }

        private Result(string message, HttpStatusCode statusCode)
            : base(false, message, statusCode)
        {
        }

        public static Result<T> Success(T data, string message = "Sucesso", HttpStatusCode statusCode = HttpStatusCode.OK)
            => new(data, message, statusCode);

        public static new Result<T> Failure(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new(message, statusCode);
    }
}
