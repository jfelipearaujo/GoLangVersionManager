using CommandLine;

using GoLangVersionManager.Commands.Interfaces;
using GoLangVersionManager.Verbs;

namespace GoLangVersionManager
{
    public class App
    {
        private readonly IInstallCommand installCommand;

        public App(IInstallCommand installCommand)
        {
            this.installCommand = installCommand;
        }

        public async Task<int> RunAsync(string[] args)
        {
            var result = Parser.Default.ParseArguments<InstallOptions, ListOptions, UseOptions>(args);

            var exitCode = await result
                .MapResult(
                    async (InstallOptions opts) => await installCommand.RunAsync(opts),
                    //async (ListOptions opts) => await RunListCommand(opts),
                    //async (UseOptions opts) => await RunUseCommand(opts),
                    errs => Task.FromResult(1));

            Console.WriteLine("Exit code: {0}", exitCode);

            return exitCode;
        }
    }
}
