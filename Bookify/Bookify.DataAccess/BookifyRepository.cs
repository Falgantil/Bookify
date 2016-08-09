using Bookify.Core;
using System;

namespace Bookify.DataAccess
{
    public class BookifyRepository : IBookifyRepository
    {
        private readonly BookifyContext _ctx;
        private static readonly BookifyRepository _instance;
        private IBookRepository _bookRepository;

        public BookifyRepository()
        {
            _ctx = new BookifyContext();
        }

        static BookifyRepository()
        {
            if (_instance == null)
            {
                _instance = new BookifyRepository();
            }
        }
        
        public static BookifyRepository Instance
        {
            get
            {
                return _instance;
            }
        }

        public IBookRepository BookRepository
        {
            get
            {
                return _bookRepository ?? (_bookRepository = new BookRepository(_ctx));
            }
        }

        public int Save()
        {
            return _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _bookRepository = null;
            _ctx.Dispose();
        }
    }
}
