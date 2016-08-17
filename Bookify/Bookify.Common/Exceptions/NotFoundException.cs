namespace Bookify.Common.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException() 
            : this("The specified resource was not found.")
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}