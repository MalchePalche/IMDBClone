using Microsoft.AspNetCore.Mvc;
using IMDBClone.Data;
using IMDBClone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace IMDBClone.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class AdminMoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminMoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Genres = new SelectList(_context.Genres.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Genres = new SelectList(_context.Genres.ToList(), "Id", "Name", movie.GenreId);
                return View(movie);
            }

            _context.Movies.Add(movie);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "✅ Movie saved successfully!";
            return RedirectToAction("Index", "AdminMovies", new { area = "Admin" });

        }
        // GET: Admin/AdminMovies/Delete/5
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var movie = _context.Movies
                .FirstOrDefault(m => m.Id == id);

            if (movie == null) return NotFound();

            return View(movie);
        }

        // POST: Admin/AdminMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _context.Movies.Find(id);

            if (movie != null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "❌ Movie deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            ViewBag.Genres = new SelectList(_context.Genres.ToList(), "Id", "Name", movie.GenreId);
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Genres = new SelectList(_context.Genres.ToList(), "Id", "Name", movie.GenreId);
                return View(movie);
            }

            _context.Movies.Update(movie);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "✅ Movie updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Genre)
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }



    }
}
