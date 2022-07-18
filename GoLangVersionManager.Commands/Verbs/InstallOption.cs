using CommandLine;

using GoLangVersionManager.Common.Interfaces;

namespace GoLangVersionManager.Commands.Verbs
{
    [Verb("install", false, HelpText = "Install (or reinstall) a valid version of Go Lang")]
    public class InstallOption : IOption
    {
        public string? Version { get; set; }
    }
}
