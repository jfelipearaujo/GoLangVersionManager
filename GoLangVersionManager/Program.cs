using CommandLine;

using GoLangVersionManager.Commands;
using GoLangVersionManager.Verbs;

var result = Parser.Default.ParseArguments<InstallOptions, ListOptions, UseOptions>(args);

var exitCode = await result
    .MapResult(
        async (InstallOptions opts) => await RunInstallCommand(opts),
        async (ListOptions opts) => await RunListCommand(opts),
        async (UseOptions opts) => await RunUseCommand(opts),
        errs => Task.FromResult(1));

Console.WriteLine("Exit code: {0}", exitCode);

async Task<int> RunInstallCommand(InstallOptions options)
{
    var installCommand = new InstallCommand(options);

    return await installCommand.Run();
}

async Task<int> RunListCommand(ListOptions options)
{
    return 0;
}

async Task<int> RunUseCommand(UseOptions options)
{
    return 0;
}
