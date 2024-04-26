
namespace OnlineShop.Core.Entities
{
    public class UploadedFile 
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string? FileName { get; set; }
        public string? ContentType { get; set; }


    }
}
