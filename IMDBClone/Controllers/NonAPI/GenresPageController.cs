using IMDBClone.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMDBClone.NonAPI
{
    public class GenresPageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GenresPageController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _context.Genres.ToListAsync();
            return View(genres);
        }
        public async Task<IActionResult> MoviesByGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .Where(m => m.GenreId == id)
                .ToListAsync();

            ViewBag.GenreName = genre.Name;
            return View(movies);
        }

    }
}
