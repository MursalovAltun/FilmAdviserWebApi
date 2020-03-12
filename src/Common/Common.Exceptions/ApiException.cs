using System;
using System.Net;

namespace Common.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            StatusCode = (int)statusCode;
        }

        public int StatusCode { get; set; }
    }
}