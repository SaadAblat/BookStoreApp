namespace BookStore.Entities
{
    public class Author
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public string Nationality { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
