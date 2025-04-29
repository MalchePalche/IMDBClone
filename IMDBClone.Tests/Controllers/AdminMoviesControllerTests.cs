using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMDBClone.Areas.Admin.Controllers;
using IMDBClone.Data; // DbContext namespace
using System;
using IMDBClone.Models;

namespace IMDBClone.Tests.Controllers
{
    [TestFixture]
    public class AdminMoviesControllerTests
    {
        private AdminMoviesController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique db each time
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new AdminMoviesController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
            _controller?.Dispose();
        }
        

        [Test]
        public void Create_Get_ReturnsViewResult()
        {
            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


        /*[Test]
        public void Create_Post_ValidMovie_RedirectsToIndex()
        {
            var movie = new Movie
            {
                Title = "Test Movie",
                Description = "Test Description",
                ReleaseYear = 2024,
                GenreId = 1,
                Director = "Test Director" // <- ADDED
            };

            var result = _controller.Create(movie) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }*/



        /*[Test]
        public void Edit_Get_NullId_ReturnsNotFound()
        {
            // Act
            var result = _controller.Edit(null);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }*/





        /*[Test]
        public void Delete_Post_ValidId_RedirectsToIndex()
        {
            var movie = new Movie
            {
                Title = "Test Delete Movie",
                Description = "Test Description",
                ReleaseYear = 2024,
                GenreId = 1,
                Director = "Test Director" // <- ADDED
            };

            _context.Movies.Add(movie);
            _context.SaveChanges();

            var result = _controller.DeleteConfirmed(movie.Id) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }*/


    }
}
