using GoLangVersionManager.Commands.Interfaces;
using GoLangVersionManager.Commands.Validators;
using GoLangVersionManager.Commands.Verbs;
using GoLangVersionManager.Common.Variables;

using System.Text.RegularExpressions;

namespace GoLangVersionManager.Commands
{
    public class UninstallCommand : IUninstallCommand
    {
        private readonly IVersionValidator versionValidator;
        private readonly IConsoleHelper consoleHelper;

        public UninstallCommand(IVersionValidator versionValidator,
            IConsoleHelper consoleHelper)
        {
            this.versionValidator = versionValidator;
            this.consoleHelper = consoleHelper;
        }

        public Task<int> RunAsync(UninstallOption option)
        {
            Console.WriteLine("Starting 'uninstall' command...");

            if (!versionValidator.IsValid(option.Version))
            {
                Console.WriteLine("The version informed '{0}' is not valid", option.Version);
                return Task.FromResult(0); // Error
            }

            var gvmDirInfo = new DirectoryInfo(BaseVariables.BASE_PATH);

            if (gvmDirInfo.Exists)
            {
                var folders = gvmDirInfo.GetDirectories()
                    .Where(x => Regex.IsMatch(x.Name, VersionValidator.VERSION_PATTERN));

                var toBeDeleted = folders.FirstOrDefault(x => x.Name.Equals(option.Version));

                if (toBeDeleted is not null)
                {
                    string? answer;

                    do
                    {
                        var question = "Do you really want to delete this version? [Y]es or [N]o: ";

                        if (consoleHelper.IsYes(question, out answer))
                        {
                            Console.Write("Deleting version {0}... ", option.Version);

                            toBeDeleted.Delete(true);

                            Console.WriteLine("Done!");

                            break;
                        }
                        else if (consoleHelper.IsNo(answer))
                        {
                            break;
                        }
                        else
                        {
                            answer = null;
                            Console.WriteLine("Invalid answer");
                        }

                    } while (string.IsNullOrEmpty(answer));
                }
                else
                {
                    Console.WriteLine("The version '{0}' is not installed", option.Version);

                    return Task.FromResult(1); // Error
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
