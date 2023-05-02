using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repositories.Admin
{
    public class GenreRepository : IDisposable
    {
        private readonly DataContext _ctx;
        public GenreRepository(DataContext ctx)
        {
            _ctx = ctx;
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public async Task<Genre> CreateGenreAsync(Genre genre)
        {
            _ctx.Genres.Add(genre);
            await _ctx.SaveChangesAsync();
            return genre;
        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            return await _ctx.Genres.FindAsync(id);
        }

        public async Task<List<Genre>> GetAllGenreAsync()
        {
            var genres = await _ctx.Genres.ToListAsync();
            return genres;
        }

        public async Task DeleteGenreAsync(int id)
        {
            var genre = await GetGenreByIdAsync(id);
            _ctx.Genres.Remove(genre);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Genre> UpdateGenreAsync(Genre updatedGenre)
        {
            var genre = await GetGenreByIdAsync(updatedGenre.ID);
            genre.Name = updatedGenre.Name;
            genre.Description = updatedGenre.Description;
            await _ctx.SaveChangesAsync();
            return genre;
        }
    }
}

