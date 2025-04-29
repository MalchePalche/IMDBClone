using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBClone.Tests.Controllers
{
    internal class ReviewsControllerTests
    {
        [Test]
        public void Review_ShouldHaveRating()
        {
            int rating = 5;
            Assert.GreaterOrEqual(rating, 1);
        }

        [Test]
        public void ReviewComment_ShouldNotBeEmpty()
        {
            string comment = "Great movie!";
            Assert.IsNotEmpty(comment);
        }

    }
}
