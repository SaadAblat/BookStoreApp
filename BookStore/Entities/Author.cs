using System.ComponentModel.DataAnnotations;

namespace BookStore.Entities
{
    public class Author
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Biography { get; set; }
        [Required]
        public int BirthDate { get; set; }
        public int DeathDate { get; set; }
         [Required]
        public string Nationality { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
