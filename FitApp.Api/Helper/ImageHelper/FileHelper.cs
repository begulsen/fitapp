using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace FitApp.Api.Helper.ImageHelper
{
    public class FileHelper :IFileHelper
    {
        public void DeleteUserPrivateImages(Guid userId, int ordinalNumber, string fileNameSuffix) 
        {
            string directory = "images/" + userId + "/";
            string imageName = "private-" + fileNameSuffix + "-image-" + ordinalNumber;
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            DirectoryInfo d = new DirectoryInfo(directory);
            FileInfo[] imageFiles = d.GetFiles();
            FileInfo oldImage = null;
            foreach (var imageFile in imageFiles)
            {
                if (Path.GetFileNameWithoutExtension(imageFile.Name) == imageName)
                {
                    oldImage = imageFile;
                    break;
                }
            }
            oldImage?.Delete();
        }

        public void UploadUserPrivateImages(Guid userId, IFormFile image, int ordinalNumber, string fileNameSuffix )
        {
            string directory = "images/" + userId + "/";
            string imageName = "private-" + fileNameSuffix + "-image-" + ordinalNumber;
            string fullPath = directory + imageName + Path.GetExtension(image.FileName);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            using (FileStream filestream = File.Create(fullPath))
            {
                image.CopyTo(filestream);
                filestream.Flush();
            }
        }
    }

    public interface IFileHelper
    {
        void DeleteUserPrivateImages(Guid userId, int ordinalNumber, string fileNameSuffix);
        public void UploadUserPrivateImages(Guid userId, IFormFile image, int ordinalNumber, string fileNameSuffix);
    }
}