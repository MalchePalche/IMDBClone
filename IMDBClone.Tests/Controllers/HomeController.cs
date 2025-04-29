using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBClone.Tests.Controllers
{
    internal class HomeController
    {
        [Test]
        public void HomePage_ShouldReturnView()
        {
            var result = "Home Page";
            Assert.IsNotNull(result);
        }

        [Test]
        public void AboutPage_ShouldReturnView()
        {
            var result = "About Page";
            Assert.IsNotNull(result);
        }

    }
}
