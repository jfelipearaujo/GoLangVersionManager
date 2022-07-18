using GoLangVersionManager.Commands.Interfaces;
using GoLangVersionManager.Common.Variables;

namespace GoLangVersionManager.Commands.Helpers
{
    public class SkipDownloadHelper : ISkipDownloadHelper
    {
        private readonly IConsoleHelper consoleHelper;

        public SkipDownloadHelper(IConsoleHelper consoleHelper)
        {
            this.consoleHelper = consoleHelper;
        }

        public bool ShouldSkipDownload(string version)
        {
            if (Directory.Exists(BaseVariables.GO_ROOT_PATH_FORMAT(version)))
            {
                string? answer;

                do
                {
                    Console.WriteLine($"Go version {version} apparently it's already installed");
                    var question = "Do you want to redownload the original files? [Y]es or [N]o: ";

                    if (consoleHelper.IsYes(question, out answer))
                    {
                        return false;
                    }
                    else if (consoleHelper.IsNo(answer))
                    {
                        Console.WriteLine("Skipping downloading Go files");
                        Console.WriteLine("Try open a new prompt and type 'go version' to test it :)");

                        return true;
                    }
                    else
                    {
                        answer = null;
                        Console.WriteLine("Invalid answer");
                    }
                } while (string.IsNullOrEmpty(answer));
            }

            return false;
        }
    }
}
