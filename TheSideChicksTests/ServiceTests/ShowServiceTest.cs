using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Models;
using TheSideChicks.Services;

namespace TheSideChicksTests.ServiceTests
{
    public class ShowServiceTest
    {

        private ShowService _sut = new ShowService();

        [Fact]
        public async Task TestGetUsersAsync()
        {

            var showList = await _sut.GetShows();

            Assert.NotNull(showList);
        }



    }
}