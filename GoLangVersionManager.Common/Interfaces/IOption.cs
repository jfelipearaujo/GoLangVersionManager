using CommandLine;

namespace GoLangVersionManager.Common.Interfaces
{
    public interface IOption
    {
        [Option('v', "version",
            Required = true,
            HelpText = "A valid version of Go Lang")]
        string Version { get; set; }
    }
}
