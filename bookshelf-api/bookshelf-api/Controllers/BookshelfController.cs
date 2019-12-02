using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookshelf_api.Auth;
using bookshelf_api.Models;
using bookshelf_api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace bookshelf_api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [ApiAuthorization]
    public class BookshelfController : ControllerBase
    {
        private readonly IBookShelf bookShelf;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BookshelfController(IBookShelf bookShelf, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.bookShelf = bookShelf;
        }

        /// <summary>
        /// Get All books
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return this.bookShelf.Books;
        }


        /// <summary>
        /// Loan books
        /// </summary>
        /// <param name="book">Book object</param>
        [Route("loan")]
        [HttpPost]
        public async Task<bool> Post([FromBody]Book book)
        {
            book.LoanedToId = int.Parse(this.httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            return await this.bookShelf.Loan(book);
        }

        /// <summary>
        /// return book
        /// </summary>
        /// <param name="book">book object</param>
        /// <returns></returns>
        [Route("return")]
        [HttpPost]
        public async Task<bool> Return([FromBody]Book book)
        {
            book.LoanedToId = int.Parse(this.httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            return await this.bookShelf.Return(book);
        }

    }
}
