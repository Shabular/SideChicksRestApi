using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TheSideChicks.Services;
using Xunit;
using Location = TheSideChicks.Models.Location;

namespace TheSideChicks.Tests
{

    public class LocationServiceTests
    {
        private readonly LocationService _sut;

        [SetUp]
        public void Setup()
        {
           
            var _sut = new LocationService();

        }

        [Test]
        public void Test_Get_Locations_DB()
        {
            var locationList = _sut.GetLocations();

            NUnit.Framework.Assert.IsNotNull(locationList);
        }
        
        [Test]
        public async void Test_CheckIfLocationExists_DB()
        {
            var location = new Location()
            {
                postalNumber = "non existing",
                number = 0
            };
            var shouldBeFalse = await _sut.CheckIfLocationExists(location);

            NUnit.Framework.Assert.IsFalse(shouldBeFalse);
        }

        

    }
}
