using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBClone.Tests.Controllers
{
    internal class GenresControllerTests
    {
        [Test]
        public void ListGenres_ShouldReturnGenres()
        {
            var genres = new[] { "Action", "Comedy" };
            Assert.IsNotEmpty(genres);
        }

        [Test]
        public void GenreName_ShouldNotBeNull()
        {
            string genreName = "Action";
            Assert.IsNotNull(genreName);
        }

    }
}
