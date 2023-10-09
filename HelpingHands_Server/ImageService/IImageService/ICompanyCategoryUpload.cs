using Microsoft.AspNetCore.Components.Forms;

namespace HelpingHands_Server.ImageService.IImageService
{
    public interface ICompanyUpload
    {
        Task<string> UploadFile(IBrowserFile file);

        bool DeleteFile(string filePath);
    }
}
