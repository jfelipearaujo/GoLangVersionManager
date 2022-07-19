using CommandLine;

using GoLangVersionManager.Common.Interfaces;

namespace GoLangVersionManager.Commands.Verbs
{
    [Verb("uninstall",
        false,
        new string[] { "un" },
        HelpText = "Uninstall a version of Go Lang")]
    public class UninstallOption : IOption
    {
        public string? Version { get; set; }
    }
}
