using GoLangVersionManager.Commands.Interfaces;

namespace GoLangVersionManager.Commands.Helpers
{
    public class ConsoleHelper : IConsoleHelper
    {
        public bool IsYes(string question, out string? answer)
        {
            Console.Write(question);
            answer = Console.ReadLine();

            if (string.IsNullOrEmpty(answer))
                return false;

            return answer.ToLower() == "y" || answer.ToLower() == "yes";
        }

        public bool IsNo(string? answer)
        {
            if (string.IsNullOrEmpty(answer))
                return false;

            return answer.ToLower() == "n" || answer.ToLower() == "no";
        }
    }
}
