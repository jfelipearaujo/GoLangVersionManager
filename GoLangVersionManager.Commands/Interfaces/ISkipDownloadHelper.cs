namespace GoLangVersionManager.Commands.Interfaces
{
    public interface ISkipDownloadHelper
    {
        bool ShouldSkipDownload(string version);
    }
}
