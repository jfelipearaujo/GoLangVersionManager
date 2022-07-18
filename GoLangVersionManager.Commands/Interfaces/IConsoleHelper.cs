namespace GoLangVersionManager.Commands.Interfaces
{
    public interface IConsoleHelper
    {
        bool IsYes(string question, out string? answer);
        bool IsNo(string? answer);
    }
}
