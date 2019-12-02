using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bookshelf_api.Models;

namespace bookshelf_api.Repository
{
    public interface IBookShelf
    {
        IEnumerable<Book> Books { get; }
        Task<bool> Loan(Book book);
        Task<bool> Return(Book book);
    }
}
