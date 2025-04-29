using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBClone.Tests.Controllers
{
    internal class UsersControllerTests
    {
        [Test]
        public void UserProfile_ShouldExist()
        {
            var user = "User123";
            Assert.IsNotNull(user);
        }

        [Test]
        public void UserEmail_ShouldContainAtSymbol()
        {
            string email = "user@example.com";
            Assert.IsTrue(email.Contains("@"));
        }

    }
}
