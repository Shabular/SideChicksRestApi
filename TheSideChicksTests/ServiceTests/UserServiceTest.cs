using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Models;
using TheSideChicks.Services;

namespace TheSideChicksTests.ServiceTests
{
    public class UserServiceTest
    {

        private UserService _sut = new UserService();

        [Fact]
        public async Task TestGetUsersAsync()
        {

            var userList = await _sut.GetUsers();

            Assert.NotNull(userList);
        }


        [Fact]
        public async Task TestCheckIfAdminAccountIsCreated()
        {
            var user = new User()
            {
                username = "admin"
            };

            var userList = await _sut.CheckIfUserInDatabase(user);

            Assert.NotNull(userList);
        }

        [Fact]
        public async Task TestIfIncompleteAddWillBeDenied()
        {
            var user = new User()
            {
                username = "admin"
            };

            var userList = await _sut.AddUserAsync(user);

            Assert.Null(userList);
        }


    }
}
