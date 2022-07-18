using CommandLine;

using GoLangVersionManager.Common.Interfaces;

namespace GoLangVersionManager.Commands.Verbs
{
    [Verb("use", false, HelpText = "Use a version of Go Lang")]
    public class UseOption : IOption
    {
        public string? Version { get; set; }
    }
}
