
using Microsoft.AspNetCore.Http;
using OnlineShop.Core.Entities;

namespace OnlineShop.Infrastructure
{
    public class FileUpload
    {
        public List<UploadedFile> UploadProductImages(List<IFormFile> Files, string userId)
        {
            List<UploadedFile> uploadedFiles = new();
            foreach (var file in Files)
            {

                UploadedFile uploadedFile = new()
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    CreatedBy = userId
                };

                var path = Path.Combine("wwwroot", "Products", file.FileName);

                using FileStream fileStream = new(path, FileMode.Create);
                file.CopyTo(fileStream);

                uploadedFiles.Add(uploadedFile);
            }

            return uploadedFiles;
        }
    }
}
