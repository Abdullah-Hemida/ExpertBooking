using ExpertBooking.Contracts.DTOs.Dashboard.ExpertDashboard;
using Microsoft.AspNetCore.Http;

namespace ExpertBooking.Application.Helper
{
     public class ConverterFileType
    {
        private async Task<FileUpload> ConvertToFileUploadAsync(IFormFile formFile)
        {
            using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);

            return new FileUpload
            {
                FileName = formFile.FileName,
                Content = memoryStream.ToArray(),
                ContentType = formFile.ContentType
            };
        }

        private async Task<List<FileUpload>> ConvertToFileUploadListAsync(List<IFormFile> files)
        {
            var uploads = new List<FileUpload>();
            foreach (var file in files)
            {
                uploads.Add(await ConvertToFileUploadAsync(file));
            }
            return uploads;
        }

    }
}
