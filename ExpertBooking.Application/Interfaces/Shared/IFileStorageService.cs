using Microsoft.AspNetCore.Http;


namespace ExpertBooking.Application.Interfaces.Shared
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName);
    }
}
