namespace GoLangVersionManager.Common.Variables
{
    public static class BaseVariables
    {
        private static string GO_BASE_PATH => "C:\\gvm\\{0}";
        private static string GO_ROOT_PATH = "{0}\\go";
        private static string GO_ROOT_BIN_PATH = "{0}\\bin";

        public static string GO_BASE_PATH_FORMAT(string goVersion)
            => string.Format(GO_BASE_PATH, goVersion);
        public static string GO_ROOT_PATH_FORMAT(string goVersion)
            => string.Format(GO_ROOT_PATH, GO_BASE_PATH_FORMAT(goVersion));

        public static string GO_ROOT_BIN_PATH_FORMAT(string goVersion)
            => string.Format(GO_ROOT_BIN_PATH, GO_ROOT_PATH_FORMAT(goVersion));
    }
}
