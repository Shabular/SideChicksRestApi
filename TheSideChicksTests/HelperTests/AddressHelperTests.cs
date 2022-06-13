using TheSideChicks.Helpers;

namespace TheSideChicksTests
{
    public class AddressHelperTests
    {
        [Fact]
        public void TestisDutchPostalCode()
        {
            AddressHelper.isDutchPostalCode("4872RE");
        }
    }
}