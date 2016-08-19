using System.Net;

namespace Bookify.Common.Exceptions
{
    public class FileAlreadyExistsException: ApiException
    {
        public FileAlreadyExistsException()
            : this("File already exists on in our database")
        {
        }

        public FileAlreadyExistsException(string message)
            : base(message)
        {
        }

        public override int StatusCode { get; } = (int)HttpStatusCode.Conflict;
    }
}
