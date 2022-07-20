using GoLangVersionManager.Commands.Interfaces;
using GoLangVersionManager.Commands.Verbs;

namespace GoLangVersionManager.Commands
{
    public class GoPathCommand : IGoPathCommand
    {
        private readonly IEnvironmentVariablesHelper environmentVariablesHelper;

        public GoPathCommand(IEnvironmentVariablesHelper environmentVariablesHelper)
        {
            this.environmentVariablesHelper = environmentVariablesHelper;
        }

        public Task<int> RunAsync(GoPathOption option)
        {
            Console.WriteLine("Starting 'gopath' command...");

            if (string.IsNullOrEmpty(option.Path))
            {
                option.Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "go");

                Console.WriteLine("No path specified, using the default one");
            }

            Console.WriteLine("Setting up the path: {0}", option.Path);

            var goPath = new DirectoryInfo(option.Path);

            if (!goPath.Exists)
            {
                Console.Write("Path does not exist, creating...");
                goPath.Create();
                Console.WriteLine("Done!");
            }

            var goPathFolders = new string[] { "bin", "pkg", "src" };

            foreach (var folder in goPathFolders)
            {
                var path = new DirectoryInfo(Path.Combine(goPath.FullName, folder));

                if (!path.Exists)
                {
                    Console.Write("Folder '{0}' does not exist, creating...", folder);
                    path.Create();
                    Console.WriteLine("Done!");
                }
            }

            var currentGoPath = environmentVariablesHelper.GetCurrentValueFromVariable("GOPATH");

            if (string.IsNullOrEmpty(currentGoPath) || !currentGoPath.Equals(option.Path))
            {
                environmentVariablesHelper.SetupEnvVariable("Setting GOPATH env variable... ", "GOPATH", option.Path);
            }
            else if (currentGoPath.Equals(option.Path))
            {
                Console.WriteLine("GOPATH already set to the given path");
            }

            Console.WriteLine("Open a new prompt to see the changes :)");

            return Task.FromResult(0); // Success
        }
    }
}
