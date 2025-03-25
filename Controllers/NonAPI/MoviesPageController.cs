using IMDBClone.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMDBClone.NonAPI 
{
    public class MoviesPageController : Controller // ✅ Renamed to avoid conflict
    {
        private readonly ApplicationDbContext _context;

        public MoviesPageController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies.Include(m => m.Genre).ToListAsync();
            return View(movies);
        }
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Reviews)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null) return NotFound();

            return View(movie);
        }

    }
}
