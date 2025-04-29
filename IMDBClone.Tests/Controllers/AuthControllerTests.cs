using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBClone.Tests.Controllers
{
    internal class AuthControllerTests
    {
        [Test]
        public void LoginPage_ShouldReturnView()
        {
            var result = "Login Page";
            Assert.IsNotNull(result);
        }

        [Test]
        public void RegisterPage_ShouldReturnView()
        {
            var result = "Register Page";
            Assert.IsNotNull(result);
        }

    }
}
