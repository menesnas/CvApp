namespace CvAI.API.Services.Abstract
{
    public interface ILangflowService
    {
        Task<string> SendCvAsync(string extractedText);
    }
}
