using Microsoft.AspNetCore.Mvc;

namespace DpAuthWebApi.Services.Common
{
    public class ServiceResponse<T>
    {
        public ServiceResponse()
        {
        }
        public ServiceResponse(string errorMessage, ErrorType error)
        {
            ErrorMessage = errorMessage;
            Error = error;
            IsSuccess = false;
        }
        public T? data { get; set; } = default(T);

        public bool IsSuccess { get; set; } = false;

        public string ErrorMessage { get; set; } = null;

        public ErrorType Error { get; set; } = ErrorType.None;
    }
}
