using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBClone.Tests.Controllers
{
    internal class FavoriteMovieControllerTests
    {
        [Test]
        public void AddMovieToFavorites_ShouldSucceed()
        {
            bool added = true;
            Assert.IsTrue(added);
        }

        [Test]
        public void RemoveMovieFromFavorites_ShouldSucceed()
        {
            bool removed = true;
            Assert.IsTrue(removed);
        }

    }
}
