using CommandLine;

using GoLangVersionManager.Interfaces;

namespace GoLangVersionManager.Verbs
{
    [Verb("use", false, HelpText = "Use a version of Go Lang")]
    public class UseOptions : IOptions
    {
        public string Version { get; set; }
    }
}
