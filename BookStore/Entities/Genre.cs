using System.ComponentModel.DataAnnotations;

namespace BookStore.Entities
{
    public class Genre
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public ICollection<BookGenre> BookGenres { get; set; }

        public bool IsFiction { get; set; }
    }

}
