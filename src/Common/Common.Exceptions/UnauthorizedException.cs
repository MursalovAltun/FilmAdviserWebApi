using System.Net;

namespace Common.Exceptions
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string message)
            : base(message, HttpStatusCode.Unauthorized)
        {
        }
    }
}