using CommandLine;

using GoLangVersionManager.Interfaces;

namespace GoLangVersionManager.Verbs
{
    [Verb("install", false, HelpText = "Install (or reinstall) a valid version of Go Lang")]
    public class InstallOptions : IOptions
    {
        public string Version { get; set; }
    }
}
