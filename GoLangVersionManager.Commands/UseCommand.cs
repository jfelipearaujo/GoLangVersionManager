using GoLangVersionManager.Commands.Interfaces;
using GoLangVersionManager.Commands.Validators;
using GoLangVersionManager.Commands.Verbs;
using GoLangVersionManager.Common.Variables;

using System.Text.RegularExpressions;

namespace GoLangVersionManager.Commands
{
    public class UseCommand : IUseCommand
    {
        private readonly IVersionValidator versionValidator;
        private readonly IEnvironmentVariablesHelper environmentVariablesHelper;

        public UseCommand(IVersionValidator versionValidator,
            IEnvironmentVariablesHelper environmentVariablesHelper)
        {
            this.versionValidator = versionValidator;
            this.environmentVariablesHelper = environmentVariablesHelper;
        }

        public Task<int> RunAsync(UseOption option)
        {
            Console.WriteLine("Starting 'use' command...");

            if (!versionValidator.IsValid(option.Version))
            {
                Console.WriteLine("The version informed '{0}' is not valid", option.Version);
                return Task.FromResult(0); // Error
            }

            var gvmDirInfo = new DirectoryInfo(BaseVariables.BASE_PATH);

            if (gvmDirInfo.Exists)
            {
                var folders = gvmDirInfo.GetDirectories()
                    .Where(x => Regex.IsMatch(x.Name, VersionValidator.VERSION_PATTERN))
                    .Select(x => x.Name);

                if (folders.Contains(option.Version))
                {
                    Console.WriteLine("Changing go version to '{0}'...", option.Version);

                    environmentVariablesHelper.SetupVariables(option.Version, true);

                    Console.WriteLine("Open a new prompt and type 'go version' to test it :)");
                }
                else
                {
                    Console.WriteLine("The version '{0}' is not installed", option.Version);
                    Console.WriteLine("Please run 'gvm install -v {0}' to install this version", option.Version);

                    return Task.FromResult(1); // Error
                }
            }
            else
            {
                Console.WriteLine("GVM is not installed");
                Console.WriteLine("Please run 'gvm install -v {0}' to install this version", option.Version);

                return Task.FromResult(1); // Error    
            }

            return Task.FromResult(0); // Success
        }
    }
}
