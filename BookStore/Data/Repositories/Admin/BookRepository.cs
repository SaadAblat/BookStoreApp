using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repositories.Admin
{
    public class BookRepository :IDisposable
    {
        private readonly DataContext _ctx;
        public BookRepository(DataContext ctx)
        {
            _ctx = ctx;
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            _ctx.Books.Add(book);
            await _ctx.SaveChangesAsync();
            return book;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            
            var book = await _ctx.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                    .FirstOrDefaultAsync(x => x.ID == id);

            return book;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            var books = await _ctx.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .ToListAsync();

            return books;
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await GetBookByIdAsync(id);
            _ctx.Books.Remove(book);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Book> UpdateBookAsync(Book updatedbook)
        {
            var book = await GetBookByIdAsync(updatedbook.ID);
            book.Title = updatedbook.Title;
            book.Price = updatedbook.Price;
            book.AuthorId= updatedbook.AuthorId;
            book.ISBN = updatedbook.ISBN;
            book.PublisherName = updatedbook.PublisherName;
            book.Description = updatedbook.Description;
            book.PublicationYear= updatedbook.PublicationYear;
            book.BookCoverUrl = updatedbook.BookCoverUrl;
            book.StockQuantity = updatedbook.StockQuantity;
            
            book.BookGenres.Clear();
            foreach(var bg in updatedbook.BookGenres)
            {
                var genre = await _ctx.Genres.FindAsync(bg.GenreId);
                if (genre != null)
                {
                    book.BookGenres.Add(new BookGenre { Book = book, Genre = genre });
                }
            }
           
            await _ctx.SaveChangesAsync();
            return book;
        }

    }
}

