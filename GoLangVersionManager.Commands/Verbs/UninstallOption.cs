using CommandLine;

namespace GoLangVersionManager.Commands.Verbs
{
    [Verb("uninstall",
        false,
        new string[] { "un" },
        HelpText = "Uninstall a version of Go Lang")]
    public class UninstallOption
    {
        [Option('v', "version",
            Required = true,
            HelpText = "A valid version of Go Lang")]
        public string? Version { get; set; }
    }
}
