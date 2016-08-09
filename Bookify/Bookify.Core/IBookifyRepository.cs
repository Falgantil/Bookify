namespace Bookify.Core
{
    public interface IBookifyRepository
    {
        IBookRepository BookRepository { get; }

        int Save();
    }
}
