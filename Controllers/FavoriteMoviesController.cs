using IMDBClone.Data;
using IMDBClone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMDBClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteMoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FavoriteMoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FavoriteMovies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteMovie>>> GetFavoriteMovies()
        {
            return await _context.FavoriteMovies
                .Include(f => f.Movie)
                .Include(f => f.User)
                .ToListAsync();
        }

        // GET: api/FavoriteMovies/User/5
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetFavoritesByUser(string userId)
        {
            var favorites = await _context.FavoriteMovies
                .Where(f => f.UserId == userId)
                .Include(f => f.Movie)
                .Select(f => f.Movie)
                .ToListAsync();

            return favorites;
        }

        // POST: api/FavoriteMovies
        [HttpPost]
        public async Task<ActionResult<FavoriteMovie>> AddFavorite(FavoriteMovie favorite)
        {
            // Check if it already exists
            var exists = await _context.FavoriteMovies.AnyAsync(f =>
                f.UserId == favorite.UserId && f.MovieId == favorite.MovieId);

            if (exists)
                return Conflict("Movie already in favorites.");

            _context.FavoriteMovies.Add(favorite);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFavoriteMovies), new { id = favorite.Id }, favorite);
        }

        // DELETE: api/FavoriteMovies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var favorite = await _context.FavoriteMovies.FindAsync(id);
            if (favorite == null) return NotFound();

            _context.FavoriteMovies.Remove(favorite);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
