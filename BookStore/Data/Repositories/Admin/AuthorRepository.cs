using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repositories.Admin
{
    public class AuthorRepository : IDisposable
    {
        private readonly DataContext _ctx;
        public AuthorRepository(DataContext ctx)
        {
            _ctx = ctx;
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            _ctx.Authors.Add(author);
            await _ctx.SaveChangesAsync();
            return author;
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _ctx.Authors.Include(a => a.Books).FirstAsync(a => a.ID == id);
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            var authors = await _ctx.Authors.Include(a => a.Books).ToListAsync();
            return authors;
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await GetAuthorByIdAsync(id);
            _ctx.Authors.Remove(author);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Author> UpdateAuthorAsync(Author updatedAuthor)
        {
            var author = await GetAuthorByIdAsync(updatedAuthor.ID);
            author.Name = updatedAuthor.Name;
            author.BirthDate = updatedAuthor.BirthDate;
            author.DeathDate = updatedAuthor.DeathDate;
            author.Nationality = updatedAuthor.Nationality;
            author.Biography = updatedAuthor.Biography;
            await _ctx.SaveChangesAsync();
            return author;
        }
    }
}
