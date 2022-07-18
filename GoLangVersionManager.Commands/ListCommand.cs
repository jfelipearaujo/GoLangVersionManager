using GoLangVersionManager.Commands.Interfaces;
using GoLangVersionManager.Commands.Validators;
using GoLangVersionManager.Commands.Verbs;
using GoLangVersionManager.Common.Variables;

using System.Text.RegularExpressions;

namespace GoLangVersionManager.Commands
{
    public class ListCommand : IListCommand
    {
        private readonly IEnvironmentVariablesHelper environmentVariablesHelper;

        public ListCommand(IEnvironmentVariablesHelper environmentVariablesHelper)
        {
            this.environmentVariablesHelper = environmentVariablesHelper;
        }

        public Task<int> RunAsync(ListOption option)
        {
            Console.WriteLine("Starting 'list' command...");

            var gvmDirInfo = new DirectoryInfo(BaseVariables.GVM_PATH);

            if (gvmDirInfo.Exists)
            {
                var folders = gvmDirInfo.GetDirectories()
                    .Where(x => Regex.IsMatch(x.Name, VersionValidator.VERSION_PATTERN))
                    .Select(x => x.Name);

                if (!folders.Any())
                {
                    Console.WriteLine("No versions found");
                    Console.WriteLine("Please run 'gvm install ...' to install a version");

                    return Task.FromResult(1); // Error    
                }
                else
                {
                    var currentGoRootBinPath = environmentVariablesHelper.GetCurrentValueFromVariable("GOROOT");
                    var currentGoVersion = string.IsNullOrEmpty(currentGoRootBinPath) ? null : currentGoRootBinPath.Split('\\')[2];

                    Console.WriteLine("Installed versions:");

                    foreach (var folder in folders)
                    {
                        if (folder.Equals(currentGoVersion))
                        {
                            Console.WriteLine("\t{0}\t\tCURRENT VERSION", folder);
                        }
                        else
                        {
                            Console.WriteLine("\t{0}", folder);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("GVM is not installed");
                Console.WriteLine("Please run 'gvm install ...' to install a version");

                return Task.FromResult(1); // Error    
            }

            return Task.FromResult(0); // Success
        }
    }
}
