using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using bookshelf_api.Common;
using bookshelf_api.Models;
using bookshelf_api.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace bookshelf_api.test
{
    public class BookShelfRepoTests
    {
        /// <summary>
        /// Get all Book test
        /// </summary>
        [Fact]
        public void GetBooksUntiTest()
        {
            var dbContextMoq = new Mock<BookshelfContext>();
            var bookList = new List<Book> {
                new Book { BookId = 1, ISBN = "123" },
                new Book { BookId = 2, ISBN = "456" }
             };
            dbContextMoq.Setup(x => x.Books).Returns(GetQueryableMockDbSet(bookList));

            var sut = new BookShelfRepo(dbContextMoq.Object);
            var booksResult = sut.Books.ToList();
            Assert.Equal(2, booksResult.Count);
        }

        /// <summary>
        /// Loan Book test
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task LoanBookUntiTest()
        {
            var loanBook = new Book { BookId = 1, ISBN = "123", LoanedToId = 1 };
            var dbContextMoq = new Mock<BookshelfContext>();
            var bookList = new List<Book> {
                new Book { BookId = 1, ISBN = "123" },
                new Book { BookId = 2, ISBN = "456" }
             };
            dbContextMoq.Setup(x => x.Books).Returns(GetQueryableMockDbSet(bookList));
            dbContextMoq.Setup(x => x.SaveChangesAsync(CancellationToken.None)).Returns(Task.FromResult(1));

            var sut = new BookShelfRepo(dbContextMoq.Object);
            var booksResult = await sut.Loan(loanBook).ConfigureAwait(false);
            Assert.True(booksResult);
        }

        /// <summary>
        /// Loan Alredy loaned book is throwing exception
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task LoanBookAlreadyLoanedUntiTest()
        {
            var loanBook = new Book { BookId = 1, ISBN = "123", LoanedToId = 1 };
            var dbContextMoq = new Mock<BookshelfContext>();
            var bookList = new List<Book> {
                new Book { BookId = 1, ISBN = "123", LoanedToId = 2 },
                new Book { BookId = 2, ISBN = "456" }
             };
            dbContextMoq.Setup(x => x.Books).Returns(GetQueryableMockDbSet(bookList));

            var sut = new BookShelfRepo(dbContextMoq.Object);
            await Assert.ThrowsAsync<ApiValidationException>(async () => await sut.Loan(loanBook).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <summary>
        /// Loan book is not exsist throwing exception
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task LoanBookNotExistingUntiTest()
        {
            var loanBook = new Book { BookId = 3, ISBN = "567", LoanedToId = 2 };
            var dbContextMoq = new Mock<BookshelfContext>();
            var bookList = new List<Book> {
                new Book { BookId = 1, ISBN = "123", LoanedToId = 2 },
                new Book { BookId = 2, ISBN = "456" }
             };
            dbContextMoq.Setup(x => x.Books).Returns(GetQueryableMockDbSet(bookList));

            var sut = new BookShelfRepo(dbContextMoq.Object);
            await Assert.ThrowsAsync<ApiValidationException>(async () => await sut.Loan(loanBook).ConfigureAwait(false)).ConfigureAwait(false);
        }

        
        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }
    }
}
