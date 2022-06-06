using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSideChicks.Controllers
{
    public partial class ImageController
    {
        [ICommand]
        public async Task<string> TakePhoto()
        {
            var localFilePath = "Error";
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
               

                if (photo != null)
                {
                    // save the file into local storage
                    try
                    {
                        localFilePath = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);

                        using Stream sourceStream = await photo.OpenReadAsync();
                        using FileStream localFileStream = File.OpenWrite(localFilePath);

                        await sourceStream.CopyToAsync(localFileStream);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    

                }
            }
            return localFilePath;
        }
        
        [ICommand]
        public async Task<string> ToBase64Photo(string localFilePath)
        {

            Byte[] bytes = File.ReadAllBytes(localFilePath);

            var fileString = Convert.ToBase64String(bytes);
        
            return fileString;
        }



        public async Task<string> FromBase64Photo(string b64Str, string type, string name)
        {
            var localFilePath = "test";
            try
            {
                localFilePath = await CreateLocation($"{name}.png", type);
                

                Byte[] bytes = Convert.FromBase64String(b64Str);

                File.WriteAllBytes(localFilePath, bytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return localFilePath;
        }


        public async Task<string> CreateLocation(string name, string type)
        {
            
            string tempPath = $"{Path.GetTempPath()}{type}";

            //var folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);

            var localFilePath = Path.Combine(tempPath, name); 

            FileInfo file = new FileInfo(localFilePath);

            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);


            return localFilePath;
        }


        [ICommand]
        public async Task<string> SavePhoto()
        {
            var localFilePath = "Error";
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();


                if (photo != null)
                {
                    // save the file into local storage
                    try
                    {
                        localFilePath = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);

                        using Stream sourceStream = await photo.OpenReadAsync();
                        using FileStream localFileStream = File.OpenWrite(localFilePath);

                        await sourceStream.CopyToAsync(localFileStream);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }


                }
            }
            return localFilePath;
        }

    }
}
