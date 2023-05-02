using BookStore.Entities;
using Microsoft.EntityFrameworkCore;


namespace BookStore.Data.Repositories.Admin
{
    public class BookRepository : IDisposable
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
            return await _ctx.Books.FindAsync(id);
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            var books = await _ctx.Books
                .Include(b => b.Author) // Include Author navigation property
                .Include(b => b.BookGenres) // Include BookGenres navigation property
                    .ThenInclude(bg => bg.Genre) // Also load Genre within BookGenre
                .ToListAsync();
            return books;
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await GetBookByIdAsync(id);
            _ctx.Books.Remove(book);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Book> UpdateBookAsync(Book updatedBook)
        {
            var book = await GetBookByIdAsync(updatedBook.ID);
            book.Title = updatedBook.Title;
            book.Price = updatedBook.Price;
            book.AuthorId = updatedBook.AuthorId;
            book.ISBN = updatedBook.ISBN;
            book.PublisherName = updatedBook.PublisherName;
            book.PublicationYear = updatedBook.PublicationYear;
            book.BookCoverUrl = updatedBook.BookCoverUrl;
            book.StockQuantity = updatedBook.StockQuantity;

            book.BookGenres = updatedBook.BookGenres;
            
            await _ctx.SaveChangesAsync();
            return book;
        }
    }
}


