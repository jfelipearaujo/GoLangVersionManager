using CommandLine;

using GoLangVersionManager.Commands.Interfaces;
using GoLangVersionManager.Commands.Verbs;

namespace GoLangVersionManager
{
    public class App
    {
        private readonly IInstallCommand installCommand;
        private readonly IListCommand listCommand;

        public App(IInstallCommand installCommand,
            IListCommand listCommand)
        {
            this.installCommand = installCommand;
            this.listCommand = listCommand;
        }

        public async Task<int> RunAsync(string[] args)
        {
            var result = Parser.Default.ParseArguments<InstallOption, ListOption, UseOption>(args);

            var exitCode = await result
                .MapResult(
                    async (InstallOption opt) => await installCommand.RunAsync(opt),
                    async (ListOption opt) => await listCommand.RunAsync(opt),
                    //async (UseOptions opts) => await RunUseCommand(opts),
                    errs => Task.FromResult(1));

            Console.WriteLine("Exit code: {0}", exitCode);

            return exitCode;
        }
    }
}
