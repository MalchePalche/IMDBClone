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
    }
}
