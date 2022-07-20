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

            var gvmDirInfo = new DirectoryInfo(BaseVariables.BASE_PATH);

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
                    var currentGoVersion = environmentVariablesHelper.GetCurrentValueFromVariable("GVM_CURRENT_GO_VERSION");

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
