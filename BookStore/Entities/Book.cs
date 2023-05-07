namespace BookStore.Entities
{
    public class Book
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public string? ISBN { get; set; }
        public string PublisherName { get; set; }
        public int PublicationYear { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? BookCoverUrl { get; set; }
        public ICollection<BookGenre> BookGenres { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
