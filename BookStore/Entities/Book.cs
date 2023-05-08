using System.ComponentModel.DataAnnotations;

namespace BookStore.Entities
{
    public class Book
    {

      
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please provide a valid author.")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public string? ISBN { get; set; }

        [Required(ErrorMessage = "PublisherName is required.")]
        public string? PublisherName { get; set; }

        [Required(ErrorMessage = "Publication year is required.")]
        public int PublicationYear { get; set; }

        [Required(ErrorMessage = "Price year is required.")]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? BookCoverUrl { get; set; }
        public ICollection<BookGenre> BookGenres { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
