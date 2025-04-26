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

        public async Task<IActionResult> Index(string title, int? genreId, int? releaseYear, string director, string actor)
        {
            var query = _context.Movies.Include(m => m.Genre).AsQueryable();

            if (!string.IsNullOrEmpty(title))
                query = query.Where(m => m.Title.Contains(title));

            if (genreId.HasValue)
                query = query.Where(m => m.GenreId == genreId.Value);

            if (releaseYear.HasValue)
                query = query.Where(m => m.ReleaseYear == releaseYear.Value);

            if (!string.IsNullOrEmpty(director))
                query = query.Where(m => m.Director.Contains(director));

            if (!string.IsNullOrEmpty(actor))
                query = query.Where(m => m.Actors.Contains(actor));

            var movies = await query.ToListAsync();
            var userId = _userManager.GetUserId(User);

            var userWatchlist = await _context.FavoriteMovies
                .Where(fm => fm.UserId == userId)
                .Select(fm => fm.MovieId)
                .ToListAsync();

            ViewBag.Watchlist = userWatchlist;

            // Pass genres and years for dropdowns
            ViewBag.Genres = await _context.Genres.ToListAsync();
            ViewBag.ReleaseYears = await _context.Movies
                .Where(m => m.ReleaseYear.HasValue)
                .Select(m => m.ReleaseYear.Value)
                .Distinct()
                .OrderByDescending(y => y)
                .ToListAsync();


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

            var user = await _userManager.GetUserAsync(User);
            bool isInWatchlist = false;

            if (user != null)
            {
                isInWatchlist = await _context.FavoriteMovies
                    .AnyAsync(f => f.UserId == user.Id && f.MovieId == movie.Id);
            }

            var viewModel = new MovieDetailsViewModel
            {
                Movie = movie,
                GenreName = movie.Genre?.Name ?? "N/A",
                Reviews = movie.Reviews?.Select(r => new ReviewDisplayViewModel
                {
                    Id = r.Id, // ✅ This is what allows delete to work
                    UserName = r.User?.UserName ?? "Unknown",
                    Rating = r.Rating,
                    Comment = r.Comment
                }).ToList() ?? new List<ReviewDisplayViewModel>(),
                Actors = movie.Actors,
                TrailerUrl = movie.TrailerUrl,
                IsInWatchlist = isInWatchlist
            };

            return View(viewModel); // ✅ return the view model with Ids included
        }



        [HttpPost]
        [Authorize] // Requires the user to be logged in
        public async Task<IActionResult> AddReview(int movieId, int rating, string comment)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                Console.WriteLine("❌ Review failed: user not logged in.");
                return Unauthorized();
            }

            if (movieId <= 0 || rating < 1 || rating > 10 || string.IsNullOrWhiteSpace(comment))
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
                Comment = comment,
                CreatedAt = DateTime.UtcNow
            };
            Console.WriteLine($"Current logged-in UserId: {userId}");

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = movieId });
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteReview(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review == null)
                return NotFound();

            var movieId = review.MovieId;

            _context.Reviews.Remove(review);
            _context.SaveChanges();

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
