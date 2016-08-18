using System.Net;

namespace Bookify.Common.Exceptions
{
    public class InvalidAccessTokenException : ApiException
    {
        public InvalidAccessTokenException()
            : this("The access token is invalid.")
        {
        }

        public InvalidAccessTokenException(string message)
            : base(message)
        {
        }

        public override int StatusCode { get; } = (int)HttpStatusCode.BadRequest;
    }
}