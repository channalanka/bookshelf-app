using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookshelf_api.Common;
using bookshelf_api.Models;

namespace bookshelf_api.Repository
{
    public class BookShelfRepo : IBookShelf
    {

        BookshelfContext db;

        public BookShelfRepo(BookshelfContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// get all books
        /// </summary>
        public IEnumerable<Book> Books => this.db.Books.ToList();

        /// <summary>
        /// Loan a book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task<bool> Loan(Book book)
        {
            var dbBook = this.db.Books.FirstOrDefault(x => x.ISBN == book.ISBN);
            //check wheather book is available and is not loaned to anyone
            if (dbBook == null)
                throw new ApiValidationException("Book is not exsist");
            if (dbBook.LoanedToId != null)
                throw new ApiValidationException("Book is already loaned ");

            dbBook.LoanedToId = book.LoanedToId;
            return await this.db.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// return a book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task<bool> Return(Book book)
        {
            var dbBook = this.db.Books.FirstOrDefault(x => x.ISBN == book.ISBN);

            Console.Write("--------LIBRARY RETURN "+ dbBook.LoanedTo);
            Console.Write(dbBook.LoanedToId);

            if (dbBook == null)
                throw new ApiValidationException("Book is not exsist");
            if (dbBook.LoanedToId == null || dbBook.LoanedToId != book.LoanedToId)
                throw new ApiValidationException("User is not match");

            dbBook.LoanedToId = (int?)null;
            dbBook.LoanedTo = null;
            return await this.db.SaveChangesAsync() > 0;
        }
    }
}
