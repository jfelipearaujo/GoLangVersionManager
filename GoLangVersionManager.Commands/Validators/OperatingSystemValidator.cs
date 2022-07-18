using GoLangVersionManager.Commands.Interfaces;

namespace GoLangVersionManager.Commands.Validators
{
    public class OperatingSystemValidator : IOperatingSystemValidator
    {
        public bool IsCurrentOperatingSystemValid(out string osName)
        {
            osName = OperatingSystem.IsWindows() ? "windows" : string.Empty;

            return string.IsNullOrEmpty(osName) ? false : true;
        }
    }
}
