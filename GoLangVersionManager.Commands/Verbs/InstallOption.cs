using CommandLine;

namespace GoLangVersionManager.Commands.Verbs
{
    [Verb("install",
        false,
        new string[] { "i" },
        HelpText = "Install (or reinstall) a valid version of Go Lang")]
    public class InstallOption
    {
        [Option('v', "version",
            Required = true,
            HelpText = "A valid version of Go Lang")]
        public string? Version { get; set; }
    }
}
