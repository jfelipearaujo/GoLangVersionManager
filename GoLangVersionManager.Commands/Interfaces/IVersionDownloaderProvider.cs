namespace GoLangVersionManager.Commands.Interfaces
{
    public interface IVersionDownloaderProvider
    {
        Task<string?> DownloadAsync(string version, string osName, string osArch);
    }
}
