using FaceAiSharpApi.Extentions;

namespace FaceAiSharpApi.Repositorys.IRepositorys
{
    public interface IFaceAiRepository
    {
        Task<AppResponse> FaceAiRegisterAsync(string jsonArray);
        Task<AppResponse> FaceAiCompareAsync(float[] embedding, int id);
    }
}
