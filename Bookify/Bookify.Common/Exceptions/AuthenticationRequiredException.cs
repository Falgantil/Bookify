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
    }
}