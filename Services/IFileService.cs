namespace DiplomMetod.Services
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile file, string folder);
    }
}
