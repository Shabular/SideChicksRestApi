namespace SideChicksRestApi.Controllers;

public class ImageController
{
      
    
    public async Task<string> FromBase64Photo(string b64Str, string imageName, string type)
    {

        Byte[] bytes = Convert.FromBase64String(b64Str);
        var filePath = $"..\\Images\\{type}\\{imageName}.png";
        FileInfo file = new FileInfo(filePath);
        file.Directory.Create(); // If the directory already exists, this method does nothing.
        
        await File.WriteAllBytesAsync(filePath, bytes);
        return filePath;
    }
    
    public async Task<string> ToBase64Photo(string localFilePath)
    {

        Byte[] bytes = File.ReadAllBytes(localFilePath);

        var fileString = Convert.ToBase64String(bytes);
        
        return fileString;
    }
}