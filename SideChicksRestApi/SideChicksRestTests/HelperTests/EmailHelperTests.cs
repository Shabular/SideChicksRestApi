using SideChicksRestApi.Helpers;

namespace SideChicksRestTests;

public class EmailHelperTests
{
    public string assertString = "Not eligable";

    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void Test_False_Email()
    {
        string fakeMail = "ThisIsNoEmail";

        var badResponse = EmailHelper.userMailCheck(fakeMail);
        Assert.AreEqual(assertString, badResponse);
    }
    
    [Test]
    public void Test_Good_Email()
    {
        string realMail = "ThisIsAEmail@hotmail.com";

        var goodResponse = EmailHelper.userMailCheck(realMail);
        Assert.AreNotEqual(assertString, goodResponse);
    }
}