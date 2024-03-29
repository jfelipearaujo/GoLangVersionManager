﻿using GoLangVersionManager.Commands.Interfaces;
using GoLangVersionManager.Common.Variables;

namespace GoLangVersionManager.Commands.Helpers
{
    public class EnvironmentVariablesHelper : IEnvironmentVariablesHelper
    {
        private readonly IConsoleHelper consoleHelper;

        public EnvironmentVariablesHelper(IConsoleHelper consoleHelper)
        {
            this.consoleHelper = consoleHelper;
        }

        public bool SetupVariables(string version, bool forceSetup)
        {
            var currentGoRoot = GetCurrentValueFromVariable("GOROOT");
            var desiredGoRoot = BaseVariables.GO_ROOT_PATH_FORMAT(version);

            if (string.IsNullOrEmpty(currentGoRoot) || forceSetup)
            {
                // Setup all
                SetupGoRootVariable(desiredGoRoot);
                SetupPathVariable(version);

                return true;
            }

            if (currentGoRoot.Equals(desiredGoRoot))
            {
                // Setup 'path' variable
                SetupPathVariable(version);

                return true;
            }

            // Ask
            string? answer;

            do
            {
                Console.WriteLine("Your GOROOT variable it's already defined as: {0}", currentGoRoot);
                var question = "We need to reset it to continue. Do you agree? [Yes] or [N]o: ";

                if (consoleHelper.IsYes(question, out answer))
                {
                    // Setup all
                    SetupGoRootVariable(desiredGoRoot);
                    SetupPathVariable(version);

                    return true;
                }
                else if (consoleHelper.IsNo(answer))
                {
                    Console.WriteLine("Cancelled by user");

                    return false;
                }
                else
                {
                    answer = null;
                    Console.WriteLine("Invalid answer");
                }
            } while (string.IsNullOrEmpty(answer));

            return true;
        }

        public string? GetCurrentValueFromVariable(string variable)
        {
            return Environment.GetEnvironmentVariable(variable, EnvironmentVariableTarget.User);
        }

        public void SetupEnvVariable(string message, string variable, string value)
        {
            Console.Write(message);

            Environment.SetEnvironmentVariable(variable,
                value,
                EnvironmentVariableTarget.User);

            Console.WriteLine("Done!");
        }

        private void SetupGoRootVariable(string value)
        {
            SetupEnvVariable("Setting GOROOT env variable... ", "GOROOT", value);
        }

        private void SetupPathVariable(string version)
        {
            var goRootBinPath = BaseVariables.GO_ROOT_BIN_PATH_FORMAT(version);

            var currentPath = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);

            if (string.IsNullOrEmpty(currentPath))
            {
                SetupEnvVariable("Adding GOROOT\\bin path to the Path... ", "Path", goRootBinPath + ";");

                SetupEnvVariable("Updating current go version... ", "GVM_CURRENT_GO_VERSION", version);
            }
            else
            {
                var paths = currentPath
                    .Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                // Check and remove if there is a differente version
                if (paths.Any(x => x.Contains(BaseVariables.BASE_PATH) && !x.Equals(goRootBinPath)))
                {
                    for (int i = 0; i < paths.Count; i++)
                    {
                        if (paths[i].Contains(BaseVariables.BASE_PATH))
                        {
                            paths.RemoveAt(i);
                            i = 0;
                        }
                    }
                }

                // Check and add if the desired version is not in the path
                if (!paths.Contains(goRootBinPath))
                {
                    paths.Add($"{goRootBinPath};");

                    var newPath = string.Join(";", paths);

                    SetupEnvVariable("Adding GOROOT\\bin path to the Path... ", "Path", newPath);

                    SetupEnvVariable("Updating current go version... ", "GVM_CURRENT_GO_VERSION", version);
                }
            }
        }
    }
}
