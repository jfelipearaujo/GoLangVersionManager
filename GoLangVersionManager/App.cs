using CommandLine;

using GoLangVersionManager.Commands.Interfaces;
using GoLangVersionManager.Commands.Verbs;

namespace GoLangVersionManager
{
    public class App
    {
        private readonly IInstallCommand installCommand;
        private readonly IListCommand listCommand;
        private readonly IUseCommand useCommand;
        private readonly IUninstallCommand uninstallCommand;

        public App(IInstallCommand installCommand,
            IListCommand listCommand,
            IUseCommand useCommand,
            IUninstallCommand uninstallCommand)
        {
            this.installCommand = installCommand;
            this.listCommand = listCommand;
            this.useCommand = useCommand;
            this.uninstallCommand = uninstallCommand;
        }

        public async Task<int> RunAsync(string[] args)
        {
            var result = Parser.Default.ParseArguments<InstallOption, ListOption, UseOption, UninstallOption>(args);

            var exitCode = await result
                .MapResult(
                    async (InstallOption opt) => await installCommand.RunAsync(opt),
                    async (ListOption opt) => await listCommand.RunAsync(opt),
                    async (UseOption opt) => await useCommand.RunAsync(opt),
                    async (UninstallOption opt) => await uninstallCommand.RunAsync(opt),
                    errs => Task.FromResult(1));

            Console.WriteLine("Exit code: {0}", exitCode);

            return exitCode;
        }
    }
}
