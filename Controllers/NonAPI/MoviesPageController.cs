using IMDBClone.Data;
using IMDBClone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMDBClone.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IMDBClone.NonAPI
{
    public class MoviesPageController : Controller
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

            return View(viewModel);
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
    }
}
