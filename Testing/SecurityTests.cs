using DAL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    [TestClass]
    public class SecurityTests
    {
        const string hashedPassword = "IUYOd7//RtQwLLcNqKIa0Geor7w2lUnDg5ueAE0NhUeRiZmS";
        [TestMethod]
        public void Compare_Different_Passwords()
        {
            Security authenticationService = new();
            Assert.IsFalse(authenticationService.ValidatePassword(hashedPassword, "Yuquita"));
        }
        [TestMethod]
        public void Compare_Equal_Passwords()
        {
            Security authenticationService = new();
            Assert.IsTrue(authenticationService.ValidatePassword(hashedPassword, "Mofongo!"));
        }
    }
}
