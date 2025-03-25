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
    }
}
