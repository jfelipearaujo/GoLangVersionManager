using CommandLine;

namespace GoLangVersionManager.Commands.Verbs
{
    [Verb("use",
        false,
        new string[] { "u" },
        HelpText = "Use a version of Go Lang")]
    public class UseOption
    {
        [Option('v', "version",
            Required = true,
            HelpText = "A valid version of Go Lang")]
        public string? Version { get; set; }
    }
}
