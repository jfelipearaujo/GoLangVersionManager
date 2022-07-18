namespace GoLangVersionManager.Commands.Interfaces
{
    public interface IVersionValidator
    {
        bool IsValid(string version);
    }
}
