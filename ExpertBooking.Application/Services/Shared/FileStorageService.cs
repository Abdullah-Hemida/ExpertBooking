using ExpertBooking.Application.Interfaces.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ExpertBooking.Application.Services.Shared
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public FileStorageService(IHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            // "wwwroot" is in the API project
            var rootPath = Path.Combine(_environment.ContentRootPath, "wwwroot");
            var uploadPath = Path.Combine(rootPath, folderName);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative URL (e.g., "/ProfileImages/uniqueFileName.jpg")
            return $"/{folderName}/{uniqueFileName}";
        }
    }
}

