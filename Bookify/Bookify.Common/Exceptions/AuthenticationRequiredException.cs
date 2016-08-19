using System.Net;

namespace Bookify.Common.Exceptions
{
    public class AuthenticationRequiredException : ApiException
    {
        public AuthenticationRequiredException()
            : this("Invalid authorization to perform this action.")
        {
        }

        public AuthenticationRequiredException(string message)
            : base(message)
        {
        }

        public override int StatusCode { get; } = (int)HttpStatusCode.Forbidden;
    }
}