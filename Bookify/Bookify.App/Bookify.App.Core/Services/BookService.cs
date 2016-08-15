using System.Collections.Generic;
using System.Threading.Tasks;

using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Models;

namespace Bookify.App.Core.Services
{
    public class BookService : IBookService
    {
        public async Task<IEnumerable<LightBookModel>> GetBooks(int index, int count)
        {
            await Task.Delay(500);

            var books = new List<LightBookModel>
            {
                new LightBookModel
                {
                    Id = 1,
                    Title = "Harry Potter and the Order of the Phoenix",
                    ThumbnailUrl =
                        "http://image.guardian.co.uk/sys-images/Books/Pix/covers/2003/09/12/Harry_packshot.jpg"
                },
                new LightBookModel
                {
                    Id = 2,
                    Title = "Game of Thrones - A song of Ice and Fire",
                    ThumbnailUrl = "https://66.media.tumblr.com/avatar_f03f6f790904_128.png"
                },
                new LightBookModel
                {
                    Id = 3,
                    Title = "Fifty Shades of Grey",
                    ThumbnailUrl = "http://65.media.tumblr.com/avatar_9cb987214c76_128.png"
                },
            };
            var array = new List<LightBookModel>();
            for (int i = index; i < index + count && books.Count > i; i++)
            {
                array.Add(books[i]);
            }
            return array;
        }

        public async Task<BookModel> GetBook(int id)
        {
            await Task.Delay(100);
            return new BookModel
            {
                Id = id,
                Chapters = 38,
                DownloadUrl =
                    "https://s3-us-west-2.amazonaws.com/pressbooks-samplefiles/MetamorphosisJacksonTheme/Metamorphosis-jackson.epub",
                CoverUrl = "http://www.harrypottertheplay.com/content/uploads/2015/10/apple-touch-icon.png",
                Title = "Metamorphosis",
                Summary = @"The PressBooks version of The Metamorphosis, by Franz Kafka. This book was produced using PressBooks.com, a simple book production tool that creates PDF, EPUB and MOBI. For more information, visit: pressbooks.com. This book is adapted from the Project Gutenberg version. It is in the public domain, and is free for the use of anyone anywhere at no cost and with almost no restrictions whatsoever. You may copy it, give it away or re-use it as you like. This book was produced using PressBooks.com, and PDF rendering was done by PrinceXML.",
                Author = "J.K. Rowling",
                OwnsBook = id % 2 == 0,
                Borrowable = id % 3 == 0
        };
    }
}
}