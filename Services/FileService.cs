
using DiplomMetod.Models;
using DiplomMetod.Shared;

namespace DiplomMetod.Services
{
    public class FileService : IFileService
    {
        public async Task<string> SaveFile(IFormFile file, string folder)
        {
            var filePath = Path.Combine($"{Const.RootPath}/{folder}", file.FileName);
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/{folder}/{file.FileName}";
        }
    }
}
