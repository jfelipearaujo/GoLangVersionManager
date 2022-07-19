using CommandLine;

namespace GoLangVersionManager.Commands.Verbs
{
    [Verb("list",
        false,
        new string[] { "l" },
        HelpText = "List all installed/downloaded versions of Go Lang")]
    public class ListOption
    {
    }
}
