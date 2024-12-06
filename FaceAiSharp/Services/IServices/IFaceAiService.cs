using FaceAiSharpApi.Extentions;

namespace FaceAiSharpApi.Services.IServices
{
    public interface IFaceAiService
    {
        Task<AppResponse> FaceAiRegisterAsync(IFormFile file);
        Task<AppResponse> FaceAiCompareAsync(IFormFile file, int id);
    }
}
