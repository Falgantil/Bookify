using System.Net;

namespace Bookify.Common.Exceptions
{
    public class InvalidCredentialsException : ApiException
    {
        public InvalidCredentialsException() : this("Invalid login credentials")
        {
        }

        public InvalidCredentialsException(string message) : base(message)
        {
        }

        public override int StatusCode { get; } = (int)HttpStatusCode.BadRequest;
    }
}