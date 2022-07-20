using CommandLine;

namespace GoLangVersionManager.Commands.Verbs
{
    [Verb("gopath",
           false,
           new string[] { "gp" },
           HelpText = "Set the GOPATH environment variable")]
    public class GoPathOption
    {
        [Option('p', "path",
            Required = false,
            HelpText = "A valid directory for the GOPATH")]
        public string? Path { get; set; }
    }
}
