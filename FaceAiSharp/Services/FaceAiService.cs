using FaceAiSharp;
using FaceAiSharpApi.Extentions;
using FaceAiSharpApi.Repositorys.IRepositorys;
using FaceAiSharpApi.Services.IServices;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FaceAiSharpApi.Services
{
    public class FaceAiService : IFaceAiService
    {
        private readonly IFaceAiRepository _faceAiRepository;
        public FaceAiService(IFaceAiRepository faceAiRepository)
        {
            _faceAiRepository = faceAiRepository;
        }

        public async Task<AppResponse> FaceAiRegisterAsync(IFormFile file)
        {
            var bytes = await file.GetBytes();

            var img = Image.Load<Rgb24>(bytes);

            var det = FaceAiSharpBundleFactory.CreateFaceDetectorWithLandmarks();
            var rec = FaceAiSharpBundleFactory.CreateFaceEmbeddingsGenerator();

            var faces = det.DetectFaces(img);

            //if (faces == null)
            //{
            //    return AppResponse.CreateFailure("No Faces Recognized!");
            //} else if (faces.Skip(1).First().Landmarks != null)
            //{
            //    return AppResponse.CreateFailure("More than 1 face Recognized!");
            //}

            var face = faces.First();

            rec.AlignFaceUsingLandmarks(img, face.Landmarks!);

            var embedding = rec.GenerateEmbedding(img);

            string jsonArray = JsonConvert.SerializeObject(embedding);
            return await _faceAiRepository.FaceAiRegisterAsync(jsonArray);
        }

        public async Task<AppResponse> FaceAiCompareAsync(IFormFile file, int id)
        {
            var bytes = await file.GetBytes();

            var img = Image.Load<Rgb24>(bytes);

            var det = FaceAiSharpBundleFactory.CreateFaceDetectorWithLandmarks();
            var rec = FaceAiSharpBundleFactory.CreateFaceEmbeddingsGenerator();

            var faces = det.DetectFaces(img);

            //if (faces == null)
            //{
            //    return AppResponse.CreateFailure("No Faces Recognized!");
            //}
            //else if (faces.Skip(1).First().Landmarks != null)
            //{
            //    return AppResponse.CreateFailure("More than 1 face Recognized!");
            //}

            var face = faces.First();

            rec.AlignFaceUsingLandmarks(img, face.Landmarks!);

            var embedding = rec.GenerateEmbedding(img);

            return await _faceAiRepository.FaceAiCompareAsync(embedding!, id);
        }
    }
}
