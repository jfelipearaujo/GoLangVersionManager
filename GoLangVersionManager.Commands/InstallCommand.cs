using GoLangVersionManager.Commands.Interfaces;
using GoLangVersionManager.Commands.Verbs;

namespace GoLangVersionManager.Commands
{
    public class InstallCommand : IInstallCommand
    {
        private readonly IVersionValidator versionValidator;
        private readonly IOperatingSystemValidator operatingSystemValidator;
        private readonly ISystemArchitectureProvider systemArchitectureProvider;
        private readonly ISkipDownloadHelper skipDownloadHelper;
        private readonly IEnvironmentVariablesHelper environmentVariablesHelper;
        private readonly IVersionDownloaderProvider versionDownloaderProvider;
        private readonly IUnpackingHelper unpackingHelper;

        public InstallCommand(IVersionValidator versionValidator,
            IOperatingSystemValidator operatingSystemValidator,
            ISystemArchitectureProvider systemArchitectureProvider,
            ISkipDownloadHelper skipDownloadHelper,
            IEnvironmentVariablesHelper environmentVariablesHelper,
            IVersionDownloaderProvider versionDownloaderProvider,
            IUnpackingHelper unpackingHelper)
        {
            this.versionValidator = versionValidator;
            this.operatingSystemValidator = operatingSystemValidator;
            this.systemArchitectureProvider = systemArchitectureProvider;
            this.skipDownloadHelper = skipDownloadHelper;
            this.environmentVariablesHelper = environmentVariablesHelper;
            this.versionDownloaderProvider = versionDownloaderProvider;
            this.unpackingHelper = unpackingHelper;
        }

        public async Task<int> RunAsync(InstallOption option)
        {
            Console.WriteLine("Starting 'install' command...");

            if (!operatingSystemValidator.IsCurrentOperatingSystemValid(out string osName))
            {
                Console.WriteLine("Sorry, but your OS '{0}' is not supported", osName);
                return 1; // Error
            }

            if (!versionValidator.IsValid(option.Version))
            {
                Console.WriteLine("The version informed '{0}' is not valid", option.Version);
                return 1; // Error
            }

            Console.WriteLine("Go Version requested: {0}", option.Version);
            Console.WriteLine("OS: {0}", osName);
            Console.WriteLine("Arch: {0}", systemArchitectureProvider.GetSystemArchitecture());

            if (skipDownloadHelper.ShouldSkipDownload(option.Version))
            {
                return 0; // Skipped with success
            }

            if (!environmentVariablesHelper.SetupVariables(option.Version, false))
            {
                return 1; // Error
            }

            var zipFile = await versionDownloaderProvider.DownloadAsync(option.Version,
                osName,
                systemArchitectureProvider.GetSystemArchitecture());

            if (string.IsNullOrEmpty(zipFile))
            {
                return 1; // Error
            }

            unpackingHelper.Unpack(zipFile, option.Version);

            Console.WriteLine("Open a new prompt and type 'go version' to test it :)");

            return 0; // Success
        }
    }
}
