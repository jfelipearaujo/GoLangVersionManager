namespace GoLangVersionManager.Commands.Interfaces
{
    public interface IEnvironmentVariablesHelper
    {
        void SetupEnvVariable(string message, string variable, string value);
        bool SetupVariables(string version, bool forceSetup);
        string? GetCurrentValueFromVariable(string variable);
    }
}
