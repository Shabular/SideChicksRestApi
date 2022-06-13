using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheSideChicks.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSideChicksTests.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        private readonly UserService _sut;

        [TestMethod()]
        public void GetUsersTest()
        {
            var userlist = _sut.GetUsers();
            Assert.IsNotNull(userlist);
        }
    }
}