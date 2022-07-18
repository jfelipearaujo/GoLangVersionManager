using GoLangVersionManager.Commands.Interfaces;

namespace GoLangVersionManager.Commands.Providers
{
    public class SystemArchitectureProvider : ISystemArchitectureProvider
    {
        public string GetSystemArchitecture()
        {
            return Environment.Is64BitOperatingSystem ? "amd64" : "386";
        }
    }
}
