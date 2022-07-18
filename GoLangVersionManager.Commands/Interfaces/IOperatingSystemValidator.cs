namespace GoLangVersionManager.Commands.Interfaces
{
    public interface IOperatingSystemValidator
    {
        bool IsCurrentOperatingSystemValid(out string osName);
    }
}
