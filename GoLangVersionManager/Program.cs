using GoLangVersionManager;
using GoLangVersionManager.Commands;
using GoLangVersionManager.Commands.Helpers;
using GoLangVersionManager.Commands.Interfaces;
using GoLangVersionManager.Commands.Providers;
using GoLangVersionManager.Commands.Validators;

using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

serviceCollection.AddHttpClient("golang", client =>
{
    client.BaseAddress = new Uri("https://go.dev");
});

serviceCollection.AddSingleton<App>();

serviceCollection.AddSingleton<IInstallCommand, InstallCommand>();

serviceCollection.AddSingleton<IVersionValidator, VersionValidator>();
serviceCollection.AddSingleton<IOperatingSystemValidator, OperatingSystemValidator>();

serviceCollection.AddSingleton<ISystemArchitectureProvider, SystemArchitectureProvider>();
serviceCollection.AddSingleton<IVersionDownloaderProvider, VersionDownloaderProvider>();

serviceCollection.AddSingleton<ISkipDownloadHelper, SkipDownloadHelper>();
serviceCollection.AddSingleton<IEnvironmentVariablesHelper, EnvironmentVariablesHelper>();
serviceCollection.AddSingleton<IUnpackingHelper, UnpackingHelper>();
serviceCollection.AddSingleton<IConsoleHelper, ConsoleHelper>();

// ---

var serviceProvider = serviceCollection.BuildServiceProvider();

var app = serviceProvider.GetRequiredService<App>();

await app.RunAsync(args);