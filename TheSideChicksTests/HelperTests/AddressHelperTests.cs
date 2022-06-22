using TheSideChicks.Helpers;

namespace TheSideChicksTests
{
    public class AddressHelperTests
    {
        [Fact]
        public void TestisDutchPostalCode()
        {
            string postalcode = "4872RE";
            var returnstring = AddressHelper.isDutchPostalCode(postalcode);

            Assert.Equal(postalcode, returnstring);
        }
    }
}