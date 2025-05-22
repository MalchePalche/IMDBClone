using IMDBClone.Data;
using IMDBClone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace IMDBClone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Featured Movies
            var featuredMovies = _context.Movies
                .Include(m => m.Genre)
                .Take(6)
                .ToList();

            // Movie of the Day
            var totalMovies = _context.Movies.Count();
            Movie movieOfTheDay = null;

            if (totalMovies > 0)
            {
                var random = new Random();
                int skip = random.Next(0, totalMovies);
                movieOfTheDay = _context.Movies
                    .Include(m => m.Genre)
                    .Skip(skip)
                    .Take(1)
                    .FirstOrDefault();
            }

            // Fun Stats
            var totalUsers = _context.Users.Count();
            var avgRating = _context.Reviews.Any() ? _context.Reviews.Average(r => r.Rating) : 0;

            ViewBag.MovieOfTheDay = movieOfTheDay;
            ViewBag.TotalMovies = totalMovies;
            ViewBag.TotalUsers = totalUsers;
            ViewBag.AverageRating = Math.Round(avgRating, 1);
            
            return View(featuredMovies);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult StatusCode(int code)
        {
            if (code == 404)
            {
                return View("NotFound");
            }

            return View("Error");
        }
        public IActionResult Crash()
        {
            throw new Exception("Simulated crash for testing 500 error page.");
        }


    }
}
