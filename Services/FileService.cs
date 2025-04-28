
using DiplomMetod.Models;

namespace DiplomMetod.Services
{
    public class FileService : IFileService
    {
        public async Task<string> SaveFile(IFormFile file, string folder)
        {
            var filePath = Path.Combine($"wwwroot/{folder}", file.FileName);
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/{folder}/{file.FileName}";
        }
    }
}
