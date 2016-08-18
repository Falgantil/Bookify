using System;

namespace Bookify.Common.Exceptions
{
    public abstract class ApiException : Exception
    {
        public ApiException()
        {
        }

        public ApiException(string message)
            : base(message)
        {
        }

        public abstract int StatusCode { get; }
    }
}