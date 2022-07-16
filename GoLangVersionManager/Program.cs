using System.IO.Compression;
using System.Net;

string goVersion;

do
{
    Console.Write("Which version of Go do you want to install? ");

    goVersion = Console.ReadLine();
} while (string.IsNullOrEmpty(goVersion));

var goBasePath = $"C:\\GoLangVersionManager\\{goVersion}";
var goRootPath = $"{goBasePath}\\go";
var goRootBinPath = $"{goRootPath}\\bin";

var os = OperatingSystem.IsWindows() ? "windows" : null;
var arch = Environment.Is64BitOperatingSystem ? "amd64" : "386";

if (string.IsNullOrEmpty(os))
{
    Console.WriteLine($"Sorry, your OS ({os}) is not supported :(");
    return;
}

var currentGoRootPath = Environment.GetEnvironmentVariable("GOROOT", EnvironmentVariableTarget.User);

if (string.IsNullOrEmpty(currentGoRootPath))
{
    SetEnvVariables();
}
else if (currentGoRootPath == goRootPath)
{
    // Ignored
}
else
{
    Console.WriteLine("Your GOROOT variable it's already defined as: {0}", currentGoRootPath);
    Console.Write("We need to reset it to continue. Do you agree? [Yes] or [N]o: ");

    var answer = Console.ReadLine();

    if (answer.ToLower() == "y")
    {
        SetEnvVariables();
    }
    else
    {
        Console.WriteLine("Cancelled by user");
        return;
    }
}

if (Directory.Exists(goBasePath))
{
    Console.WriteLine($"Go version {goVersion} apparently it's already installed");
    Console.Write("Do you want to redownload the original files? [Y]es or [N]o: ");

    var answer = Console.ReadLine();

    if (answer.ToLower() == "y")
    {
        await DownloadGoFiles();
    }
    else
    {
        Console.WriteLine("Skipping downloading Go files");
    }
}
else
{
    await DownloadGoFiles();
}

Console.WriteLine("Done");
Console.ReadKey();

void SetEnvVariables()
{
    Console.WriteLine("Setting GOROOT env variable for the User...");

    Environment.SetEnvironmentVariable("GOROOT", goRootPath, EnvironmentVariableTarget.User);

    Console.WriteLine("Adding GOROOT to the Path for the User...");

    var currentPath = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);

    if (string.IsNullOrEmpty(currentPath))
    {
        Console.WriteLine("Path is empty");

        Environment.SetEnvironmentVariable("Path", $"{goRootBinPath};", EnvironmentVariableTarget.User);
    }
    else
    {
        var pathValues = currentPath
            .Split(';', StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        for (int i = 0; i < pathValues.Count; i++)
        {
            if (pathValues[i].Contains("GoLangVersionManager"))
            {
                pathValues.RemoveAt(i);
                i = 0;
            }
        }

        if (!pathValues.Contains(goRootBinPath))
        {
            pathValues.Add($"{goRootBinPath};");

            var newPath = string.Join(";", pathValues);

            Environment.SetEnvironmentVariable("Path", newPath, EnvironmentVariableTarget.User);
        }
    }
}

async System.Threading.Tasks.Task DownloadGoFiles()
{
    Console.WriteLine($"Downloading go {goVersion} version...");

    var uri = new Uri($"https://go.dev/dl/go{goVersion}.{os}-{arch}.zip");

    var httpClient = new HttpClient();

    var response = await httpClient.GetAsync(uri);

    if (response.StatusCode == HttpStatusCode.NotFound)
    {
        Console.WriteLine("Sorry, but the go version {0} wanst found :(", goVersion);
        return;
    }
    else if (response.StatusCode != HttpStatusCode.OK)
    {
        Console.WriteLine("Sorry, but an error ({0}) occurred while downloading the file :(", response.StatusCode);
        return;
    }

    var file = Path.Combine(Environment.CurrentDirectory, $"go{goVersion}.{os}-{arch}.zip");

    if (File.Exists(file))
    {
        File.Delete(file);
    }

    using (var fs = new FileStream(file, FileMode.CreateNew))
    {
        await response.Content.CopyToAsync(fs);
    }

    Console.WriteLine($"Downloading go {goVersion} version... Done!");

    Console.WriteLine("Extracting files...");

    ZipFile.ExtractToDirectory(file, goBasePath, true);

    Console.WriteLine("Extracting files... Done!");
}

