using System.Net;

namespace Common.Exceptions
{
    public class BadRequestException : ApiException
    {
        public BadRequestException(string message)
            : base(message, HttpStatusCode.BadRequest)
        {
        }
    }
}