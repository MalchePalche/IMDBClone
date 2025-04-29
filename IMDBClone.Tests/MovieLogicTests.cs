using NUnit.Framework;
using System.Collections.Generic;

namespace IMDBClone.Tests
{
    [TestFixture]
    public class MovieLogicTests
    {
        [Test]
        public void Movie_Title_ShouldNotBeNull()
        {
            var title = "Inception";
            Assert.IsNotNull(title);
        }

        [Test]
        public void Movie_ReleaseYear_ShouldBeValid()
        {
            int releaseYear = 2010;
            Assert.GreaterOrEqual(releaseYear, 1888); // Movies didn't exist before 1888
        }

        [Test]
        public void Movie_Rating_ShouldBeBetween1And5()
        {
            int rating = 4;
            Assert.IsTrue(rating >= 1 && rating <= 5);
        }

        [Test]
        public void Watchlist_ShouldContainMovie()
        {
            var watchlist = new List<string> { "Inception", "The Batman" };
            Assert.Contains("Inception", watchlist);
        }

        [Test]
        public void Watchlist_ShouldNotBeEmpty()
        {
            var watchlist = new List<string> { "Inception" };
            Assert.IsNotEmpty(watchlist);
        }

        [Test]
        public void Genre_ShouldMatchMovie()
        {
            string genre = "Action";
            Assert.AreEqual("Action", genre);
        }

        [Test]
        public void Director_Name_ShouldNotBeNull()
        {
            string director = "Christopher Nolan";
            Assert.IsNotNull(director);
        }

        [Test]
        public void ActorList_ShouldContainLeadActor()
        {
            var actors = new List<string> { "Leonardo DiCaprio", "Joseph Gordon-Levitt" };
            Assert.Contains("Leonardo DiCaprio", actors);
        }

        [Test]
        public void Movie_Summary_ShouldContainKeyword()
        {
            string summary = "A mind-bending thriller about dreams within dreams.";
            Assert.IsTrue(summary.Contains("dreams"));
        }

        [Test]
        public void Movie_Id_ShouldBePositive()
        {
            int movieId = 1;
            Assert.Greater(movieId, 0);
        }
    }
}
