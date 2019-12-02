using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshelf_api.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        [ForeignKey("User")]
        public int? LoanedToId { get; set; }
        public User LoanedTo { get; set; }
    }
}
