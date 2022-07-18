using GoLangVersionManager.Utils;
using GoLangVersionManager.Verbs;

using System.IO.Compression;
using System.Net;
using System.Text.RegularExpressions;

namespace GoLangVersionManager.Commands
{
    public class InstallCommand
    {
        private const string VERSION_PATTERN = @"^[1-9]\.[1-9][0-9](\.[1-9][0-9]|\.[1-9])?$";

        private readonly string? OS;
        private readonly string ARCH;
        private readonly InstallOptions options;

        public InstallCommand(InstallOptions options)
        {
            this.options = options;

            OS = OperatingSystem.IsWindows() ? "windows" : null;
            ARCH = Environment.Is64BitOperatingSystem ? "amd64" : "386";
        }

        public async Task<int> Run()
        {
            try
            {
                var goVersion = options.Version;

                var isMatch = Regex.IsMatch(goVersion, VERSION_PATTERN);

                if (!isMatch)
                {
                    Console.WriteLine("The version informed '{0}' is not valid", goVersion);
                    return 1;
                }

                if (string.IsNullOrEmpty(OS))
                {
                    Console.WriteLine("Sorry, but your OS '{0}' is not supported", OS);
                    return 1;
                }

                Console.WriteLine("Go Version: {0}", goVersion);
                Console.WriteLine("OS: {0}", OS);
                Console.WriteLine("Arch: {0}", ARCH);

                bool skippDownload = false;

                if (Directory.Exists(string.Concat(BaseVars.GoBasePath, goVersion)))
                {
                    string? answer = null;

                    do
                    {
                        Console.WriteLine($"Go version {goVersion} apparently it's already installed");
                        Console.Write("Do you want to redownload the original files? [Y]es or [N]o: ");

                        answer = Console.ReadLine();

                        if (answer.ToLower() == "y")
                        {
                            skippDownload = false;
                        }
                        else if (answer.ToLower() == "n")
                        {
                            Console.WriteLine("Skipping downloading Go files");
                            skippDownload = true;
                        }

                    } while (string.IsNullOrEmpty(answer));
                }

                var envVarsResult = EnvVars.SetupGoRootEnvVariable(goVersion);

                if (!envVarsResult)
                    return 1;

                if (skippDownload)
                {
                    Console.WriteLine("Try open a new prompt and type 'go version' to test it :)");
                    return 0;
                }

                Console.WriteLine($"Downloading go {goVersion} version...");

                var uri = new Uri($"https://go.dev/dl/go{goVersion}.{OS}-{ARCH}.zip");

                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync(uri);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Sorry, but the go version {0} wans't found :(", goVersion);
                    return 1;
                }

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine("Sorry, but an error ({0}) occurred while downloading the file :(", response.StatusCode);
                    return 1;
                }

                var file = Path.Combine(Environment.CurrentDirectory, $"go{goVersion}.{OS}-{ARCH}.zip");

                if (File.Exists(file))
                {
                    File.Delete(file);
                }

                using (var fs = new FileStream(file, FileMode.CreateNew))
                {
                    await response.Content.CopyToAsync(fs);
                }

                Console.WriteLine($"Downloading go {goVersion} version... Done!");

                Console.WriteLine("Extracting files...");

                ZipFile.ExtractToDirectory(file, string.Concat(BaseVars.GoBasePath, goVersion), true);

                Console.WriteLine("Extracting files... Done!");

                Console.WriteLine("Open a new prompt and type 'go version' to test it :)");

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: {0}", ex.Message);
                return 1;
            }
        }
    }
}
