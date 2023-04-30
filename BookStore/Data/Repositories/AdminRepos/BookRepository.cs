using BookStore.Entities;

namespace BookStore.Data.Repositories.AdminRepos
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
            await _ctx.AddAsync(book);
            await _ctx.SaveChangesAsync();
            return book;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _ctx.Books.FindAsync(id);
        }

        public async Task DeleteBookByIdAsync(int id)
        {
            var book = await GetBookByIdAsync(id);
            if (book != null)
            {
                _ctx.Books.Remove(book);
                await _ctx.SaveChangesAsync();
            }
        }
        public async Task UpdateBookAsync(Book book)
        {
            _ctx.Books.Update(book);
            await _ctx.SaveChangesAsync();
        }


    }
}
