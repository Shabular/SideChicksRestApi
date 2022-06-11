using SideChicksRestApi.Controllers;

namespace SideChicksRestTests;

public class ImageControllerTest
{
    
    ImageController imageController = new ImageController();

    private string sidechicksBase64StringStart =
        "iVBORw0KGgoAAAANSUhEUgAABPsAAANZCAYAAAB5lztcAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAP";
    
    bool assertion = false;
    
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public async Task Test_Image_to_Base64()
    {
        var filePath = $"..\\..\\..\\Images\\sidechicks.png";
        var base64String = await imageController.ToBase64Photo(filePath);
        if (base64String.StartsWith(sidechicksBase64StringStart))
            assertion = true;

        Assert.IsTrue(assertion);
    }

    
    
}