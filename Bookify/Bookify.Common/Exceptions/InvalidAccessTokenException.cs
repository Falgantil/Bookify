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
    }
}