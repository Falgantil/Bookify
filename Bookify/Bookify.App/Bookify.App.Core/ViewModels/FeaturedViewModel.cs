using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bookify.App.Core.Collections;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Sdk.Interfaces;
using Bookify.Common.Filter;
using Bookify.Common.Models;
using Polly;

namespace Bookify.App.Core.ViewModels
{
    public class FeaturedViewModel : BaseViewModel
    {
        private readonly IBooksApi booksApi;

        public FeaturedViewModel(IBooksApi booksApi)
        {
            this.booksApi = booksApi;
            var filter = new BookFilter
            {
                Count = 2
            };
            this.Books = new ObservableApiCollection<BookDto, BookFilter, IBooksApi>(this.booksApi, filter);
        }

        public ObservableApiCollection<BookDto, BookFilter, IBooksApi> Books { get; }

        public async Task<DetailedBookDto> GetBook(int id)
        {
            return await this.booksApi.Get(id);
        }
    }
}