namespace BookStore.Entities
{
    public class Genre
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<BookGenre> BookGenres { get; set; }
    }

}
