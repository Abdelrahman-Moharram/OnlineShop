
using Microsoft.AspNetCore.Http;
using OnlineShop.Core.Entities;

namespace OnlineShop.Infrastructure
{
    public class FileUpload
    {
        public List<ProductFile> UploadProductImages(List<IFormFile> Files)
        {
            List<ProductFile> uploadedFiles = new();
            foreach (var file in Files)
            {

                ProductFile uploadedFile = new()
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                };

                var path = Path.Combine("wwwroot", "Products", file.FileName);

                using FileStream fileStream = new(path, FileMode.Create);
                file.CopyTo(fileStream);

                uploadedFiles.Add(uploadedFile);
            }

            return uploadedFiles;
        }

        public List<Banner> UploadBannerImages(List<IFormFile> Files)
        {
            List<Banner> uploadedFiles = new();

            foreach (var file in Files)
            {
                Banner bannerFile = new() 
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                };

                var path = Path.Combine("wwwroot", "Banner", file.FileName);

                using FileStream fileStream = new(path, FileMode.Create);
                file.CopyTo(fileStream);

                uploadedFiles.Add(bannerFile);

            }

            return uploadedFiles ;
        }
    }
}
