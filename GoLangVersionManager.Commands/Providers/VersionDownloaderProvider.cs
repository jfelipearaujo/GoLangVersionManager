using GoLangVersionManager.Commands.Interfaces;

namespace GoLangVersionManager.Commands.Providers
{
    public class VersionDownloaderProvider : IVersionDownloaderProvider
    {
        private readonly IHttpClientFactory httpClientFactory;

        public VersionDownloaderProvider(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<string?> DownloadAsync(string version, string osName, string osArch)
        {
            try
            {
                var versionName = string.Concat("go", version, ".", osName, "-", osArch);

                Console.Write($"Downloading version '{versionName}'... ");

                var httpClient = httpClientFactory.CreateClient("golang");

                var response = await httpClient.GetAsync($"/dl/{versionName}.zip");

                if (response.IsSuccessStatusCode)
                {
                    var zipFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), $"{versionName}.zip");

                    if (File.Exists(zipFile))
                    {
                        File.Delete(zipFile);
                    }

                    using (var fs = new FileStream(zipFile, FileMode.CreateNew))
                    {
                        await response.Content.CopyToAsync(fs);
                    }

                    Console.WriteLine("Done");

                    return zipFile;
                }
                else
                {
                    Console.WriteLine("Failed to download the file: {0}", response.StatusCode);

                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when downloading the version {0}: {1}", version, ex.Message);

                return null;
            }
        }
    }
}
