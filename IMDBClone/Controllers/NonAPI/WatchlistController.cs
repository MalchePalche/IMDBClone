using IMDBClone.Data;
using IMDBClone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMDBClone.NonAPI
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class WatchlistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public WatchlistController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Add(int movieId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            bool exists = await _context.FavoriteMovies
                .AnyAsync(w => w.UserId == user.Id && w.MovieId == movieId);

            if (!exists)
            {
                _context.FavoriteMovies.Add(new FavoriteMovie
                {
                    UserId = user.Id,
                    MovieId = movieId
                });

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "MoviesPage");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int movieId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var watchEntry = await _context.FavoriteMovies
                .FirstOrDefaultAsync(w => w.UserId == user.Id && w.MovieId == movieId);

            if (watchEntry != null)
            {
                _context.FavoriteMovies.Remove(watchEntry);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "MoviesPage");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var watchlist = await _context.FavoriteMovies
                .Include(f => f.Movie)
                .Where(f => f.UserId == user.Id)
                .Select(f => f.Movie)
                .ToListAsync();

            return View(watchlist);
        }

    }
}
