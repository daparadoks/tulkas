namespace Tulkas.Core.Components
{
    public class ApiResponse<T>:ApiResponse
    {
        public ApiResponse(T data) : base()
        {
            Data = data;
            Success = true;
        }

        public ApiResponse(string message, bool success = false):base(message,success)
        {
        }

        public ApiResponse() : base()
        {
            Success = true;
        }
        public T Data { get; set; }
    }

    public class ApiResponse
    {
        public ApiResponse(string message, bool success = false)
        {
            Success = success;
            Message = message;
        }

        public ApiResponse(CustomInformationException exception)
        {
            Success = exception.Success;
            Message = exception.Message;
        }

        public ApiResponse()
        {
            Success = true;
        }
        
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}