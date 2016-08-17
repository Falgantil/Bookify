using System.Threading.Tasks;

using Bookify.Common.Commands.Auth;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Bookify.DataAccess.Models;
using Bookify.DataAccess.Models.ViewModels;

namespace Bookify.DataAccess.Interfaces.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<BookDto> GetById(int id);

        Task<IPaginatedEnumerable<BookDto>> GetByFilter(BookFilter filter);

        Task<BookStatisticsDto> FindForStatistics(int id);

        Task<DetailedBookDto> CreateBook(CreateBookCommand command);

        Task<DetailedBookDto> EditBook(UpdateBookCommand command);
    }
}
