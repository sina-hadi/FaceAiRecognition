namespace FaceAiSharpApi.Extentions
{
    public class AppResponse
    {
        private AppResponse(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public static AppResponse CreateSuccess()
        {
            return new AppResponse(true, string.Empty);
        }

        public static AppResponse CreateFailure(string message)
        {
            return new AppResponse(false, message);
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
