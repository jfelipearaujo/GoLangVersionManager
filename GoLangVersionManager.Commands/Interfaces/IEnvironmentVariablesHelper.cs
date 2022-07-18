namespace GoLangVersionManager.Commands.Interfaces
{
    public interface IEnvironmentVariablesHelper
    {
        string? GetCurrentValueFromVariable(string variable);
        bool SetupVariables(string version, bool forceSetup);
    }
}
