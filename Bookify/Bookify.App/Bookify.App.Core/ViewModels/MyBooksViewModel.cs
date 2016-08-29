using System.Threading.Tasks;
using Bookify.App.Core.Collections;
using Bookify.App.Core.Interfaces.Services;
using Bookify.Common.Filter;
using Bookify.Common.Models;

namespace Bookify.App.Core.ViewModels
{
    public class MyBooksViewModel : BaseViewModel
    {
        private readonly IBooksService booksService;

        public MyBooksViewModel(IBooksService booksService)
        {
            this.booksService = booksService;
            this.Books = this.booksService.MyBooks;
        }

        public ObservableServiceCollection<BookDto, BookFilter, IBooksService> Books { get; }

        public async Task<DetailedBookDto> GetBook(int id)
        {
            return await this.booksService.GetBook(id);
        }
    }
}