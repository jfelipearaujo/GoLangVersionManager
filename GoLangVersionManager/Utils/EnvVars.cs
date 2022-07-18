namespace GoLangVersionManager.Utils
{
    public static class EnvVars
    {
        public static bool SetupGoRootEnvVariable(string goVersion)
        {
            var currentGoRootPath = Environment.GetEnvironmentVariable("GOROOT", EnvironmentVariableTarget.User);

            var goRootPath = string.Concat(BaseVars.GoBasePath, goVersion, BaseVars.GoRootPath);
            var goRootBinPath = string.Concat(BaseVars.GoBasePath, goVersion, BaseVars.GoRootPath, BaseVars.GoRootBinPath);

            if (string.IsNullOrEmpty(currentGoRootPath))
            {
                return SetGoRootVariable(goRootPath) && CreateOrUpdatePathVariable(goRootBinPath);
            }
            else if (currentGoRootPath == goRootPath)
            {
                return CreateOrUpdatePathVariable(goRootBinPath);
            }
            else
            {
                string? answer = null;

                do
                {
                    Console.WriteLine("Your GOROOT variable it's already defined as: {0}", currentGoRootPath);
                    Console.Write("We need to reset it to continue. Do you agree? [Yes] or [N]o: ");

                    answer = Console.ReadLine();

                    if (answer.ToLower() == "y")
                    {
                        return SetGoRootVariable(goRootPath) && CreateOrUpdatePathVariable(goRootBinPath);
                    }
                    else if (answer.ToLower() == "n")
                    {
                        Console.WriteLine("Cancelled by user");
                        return false;
                    }

                } while (string.IsNullOrEmpty(answer));

                return false;
            }
        }

        private static void PrintMessageAndSetVariable(string message, string variable, string value)
        {
            Console.WriteLine(message);
            Environment.SetEnvironmentVariable(variable, value, EnvironmentVariableTarget.User);
            Console.WriteLine($"{message} Done!");
        }

        private static bool SetGoRootVariable(string goRootPath)
        {
            PrintMessageAndSetVariable("Setting GOROOT env variable for the User...", "GOROOT", goRootPath);

            return true;
        }

        private static bool CreateOrUpdatePathVariable(string goRootBinPath)
        {
            var currentPath = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);

            if (string.IsNullOrEmpty(currentPath))
            {
                PrintMessageAndSetVariable("Adding GOROOT\\bin path to the Path for the User...", "Path", $"{goRootBinPath};");
            }
            else
            {
                var pathValues = currentPath
                    .Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                // Check and remove if there is a differente version
                if (pathValues.Any(x => x.Contains("C:\\gvm\\") && !x.Equals(goRootBinPath)))
                {
                    for (int i = 0; i < pathValues.Count; i++)
                    {
                        if (pathValues[i].Contains("C:\\gvm\\"))
                        {
                            pathValues.RemoveAt(i);
                            i = 0;
                        }
                    }
                }

                // Check and add if the desired version is already in the path
                if (!pathValues.Contains(goRootBinPath))
                {
                    pathValues.Add($"{goRootBinPath};");

                    var newPath = string.Join(";", pathValues);

                    PrintMessageAndSetVariable("Adding GOROOT\\bin path to the Path for the User...", "Path", newPath);
                }
            }

            return true;
        }
    }
}
