using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using bookshelf_api.Controllers;
using bookshelf_api.Models;
using bookshelf_api.Repository;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace bookshelf_api.test
{
    public class BookshelfControllerTests
    {
        [Fact]
        public void GetBooksUntiTest()
        {
            var contextMoq = new Mock<IHttpContextAccessor>();
            var bookMoq = new Mock<IBookShelf>();
            bookMoq.Setup(x => x.Books).Returns(new List<Book> {
                new Book { BookId = 1, ISBN = "123" },
                new Book { BookId = 2, ISBN = "456" }
            });

            var sut = new BookshelfController(bookMoq.Object, contextMoq.Object);
            var booksResult = sut.Get().ToList();
            Assert.Equal(2, booksResult.Count);
        }

        [Fact]
        public async Task LoanBookUnitTest()
        {
            var contextAccessMoq = new Mock<IHttpContextAccessor>();
            var contextMoq = new Mock<HttpContext>();
            var claimsPrincipalMoq = new Mock<ClaimsPrincipal>();
            claimsPrincipalMoq.Setup(x => x.Claims).Returns(new List<Claim>
            {
                new Claim("userId", "1")
            });
            contextMoq.Setup(x => x.User).Returns(claimsPrincipalMoq.Object);
            contextAccessMoq.Setup(x => x.HttpContext).Returns(contextMoq.Object);
            Book book = new Book { BookId = 1, ISBN = "123" };
            var bookMoq = new Mock<IBookShelf>();
            bookMoq.Setup(x => x.Loan(book)).Returns(Task.FromResult(true));

            var sut = new BookshelfController(bookMoq.Object, contextAccessMoq.Object);
            var booksResult = await sut.Post(book).ConfigureAwait(false);
            Assert.True(booksResult);
        }

        [Fact]
        public async Task ReturnBookUnitTest()
        {
            var contextAccessMoq = new Mock<IHttpContextAccessor>();
            var contextMoq = new Mock<HttpContext>();
            var claimsPrincipalMoq = new Mock<ClaimsPrincipal>();
            claimsPrincipalMoq.Setup(x => x.Claims).Returns(new List<Claim>
            {
                new Claim("userId", "1")
            });
            contextMoq.Setup(x => x.User).Returns(claimsPrincipalMoq.Object);
            contextAccessMoq.Setup(x => x.HttpContext).Returns(contextMoq.Object);
            Book book = new Book { BookId = 1, ISBN = "123" };
            var bookMoq = new Mock<IBookShelf>();
            bookMoq.Setup(x => x.Return(book)).Returns(Task.FromResult(true));

            var sut = new BookshelfController(bookMoq.Object, contextAccessMoq.Object);
            var booksResult = await sut.Return(book).ConfigureAwait(false);
            Assert.True(booksResult);
        }
    }
}
