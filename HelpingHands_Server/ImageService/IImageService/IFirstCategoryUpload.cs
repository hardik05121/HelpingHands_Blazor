using Microsoft.AspNetCore.Components.Forms;

namespace HelpingHands_Server.ImageService.IImageService
{
    public interface IFirstCategoryUpload
    {
        Task<string> UploadFile(IBrowserFile file);

        bool DeleteFile(string filePath);
    }
}
