namespace GoLangVersionManager.Common.Variables
{
    public static class BaseVariables
    {
        public static string BASE_PATH => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "versions");

        public static string GO_BASE_PATH_FORMAT(string goVersion)
            => Path.Combine(BASE_PATH, goVersion);

        public static string GO_ROOT_PATH_FORMAT(string goVersion)
            => Path.Combine(GO_BASE_PATH_FORMAT(goVersion), "go");

        public static string GO_ROOT_BIN_PATH_FORMAT(string goVersion)
            => Path.Combine(GO_ROOT_PATH_FORMAT(goVersion), "bin");
    }
}
