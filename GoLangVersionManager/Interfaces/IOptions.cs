using CommandLine;

namespace GoLangVersionManager.Interfaces
{
    public interface IOptions
    {
        [Option('v', "version",
            Required = true,
            HelpText = "A valid version of Go Lang")]
        string Version { get; set; }
    }
}
