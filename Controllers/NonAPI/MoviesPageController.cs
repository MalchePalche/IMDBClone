using IMDBClone.Data;
using IMDBClone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMDBClone.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace IMDBClone.NonAPI
{
    public class MoviesPageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public MoviesPageController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies.Include(m => m.Genre).ToListAsync();
            var userId = _userManager.GetUserId(User);

            var userWatchlist = await _context.FavoriteMovies
                .Where(fm => fm.UserId == userId)
                .Select(fm => fm.MovieId)
                .ToListAsync();

            ViewBag.Watchlist = userWatchlist;

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

            var viewModel = new MovieDetailsViewModel
            {
                Movie = movie,
                GenreName = movie.Genre?.Name ?? "Unknown",
                Reviews = movie.Reviews.Select(r => new ReviewDisplayViewModel
                {
                    Rating = r.Rating,
                    Comment = r.Comment,
                    UserName = r.User?.UserName ?? "Unknown"
                }).ToList()
            };
            return View(new MovieDetailsViewModel
            {
                Movie = movie,
                GenreName = movie.Genre?.Name,
                Reviews = movie.Reviews?
                .Select(r => new ReviewDisplayViewModel
                {
                    Rating = r.Rating,
                    Comment = r.Comment,
                    UserName = r.User?.UserName ?? "Unknown"
                }).ToList(),
                Actors = movie.Actors,
                TrailerUrl = movie.TrailerUrl
            });


            
        }


        [HttpPost]
        [Authorize] // Requires the user to be logged in
        public async Task<IActionResult> AddReview(int movieId, int rating, string comment)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (movieId <= 0 || string.IsNullOrWhiteSpace(userId) || rating < 1 || rating > 10 || string.IsNullOrWhiteSpace(comment))
            {
                Console.WriteLine("❌ Invalid review submission:");
                Console.WriteLine($"MovieId={movieId}, UserId='{userId}', Rating={rating}, Comment='{comment}'");
                return BadRequest("Invalid data");
            }

            var review = new Review
            {
                MovieId = movieId,
                UserId = userId,
                Rating = rating,
                Comment = comment
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = movieId });
        }
        [HttpPost]
        public async Task<IActionResult> AddToWatchlist(int movieId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var exists = await _context.FavoriteMovies.AnyAsync(f => f.UserId == userId && f.MovieId == movieId);
            if (!exists)
            {
                var favorite = new FavoriteMovie { UserId = userId, MovieId = movieId };
                _context.FavoriteMovies.Add(favorite);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWatchlist(int movieId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var favorite = await _context.FavoriteMovies.FirstOrDefaultAsync(f => f.UserId == userId && f.MovieId == movieId);
            if (favorite != null)
            {
                _context.FavoriteMovies.Remove(favorite);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

    }
}
