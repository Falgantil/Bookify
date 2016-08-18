using System.Net;

namespace Bookify.Common.Exceptions
{
    public class BadRequestException : ApiException
    {
        public BadRequestException()
            : this("The request could not be processed due to invalid data.")
        {
        }

        public BadRequestException(string message)
            : base(message)
        {
        }

        public override int StatusCode { get; } = (int)HttpStatusCode.BadRequest;
    }
}