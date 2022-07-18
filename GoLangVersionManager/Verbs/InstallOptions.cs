using CommandLine;

using GoLangVersionManager.Common.Interfaces;

namespace GoLangVersionManager.Verbs
{
    [Verb("install", false, HelpText = "Install (or reinstall) a valid version of Go Lang")]
    public class InstallOptions : IOption
    {
        public string Version { get; set; }
    }
}
