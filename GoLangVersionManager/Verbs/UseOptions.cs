using CommandLine;

using GoLangVersionManager.Common.Interfaces;

namespace GoLangVersionManager.Verbs
{
    [Verb("use", false, HelpText = "Use a version of Go Lang")]
    public class UseOptions : IOption
    {
        public string Version { get; set; }
    }
}
