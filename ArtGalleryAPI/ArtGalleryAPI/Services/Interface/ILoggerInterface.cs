namespace ArtGalleryAPI.Services.Interface
{
    public interface ILoggerInterface
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}
