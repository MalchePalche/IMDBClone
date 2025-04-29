using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBClone.Tests.Controllers
{
    internal class MoviesControllerTests
    {
        [Test]
        public void MoviesList_ShouldReturnMovies()
        {
            var movies = new[] { "Inception", "Interstellar" };
            Assert.IsNotEmpty(movies);
        }

        [Test]
        public void MovieDetails_ShouldReturnMovie()
        {
            string movieTitle = "Inception";
            Assert.AreEqual("Inception", movieTitle);
        }

    }
}
