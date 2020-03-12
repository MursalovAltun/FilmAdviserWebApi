namespace Common.DTO
{
    public class ApiExceptionDTO
    {
        public ApiExceptionDTO(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public string Message { get; set; }

        public int StatusCode { get; set; }
    }
}