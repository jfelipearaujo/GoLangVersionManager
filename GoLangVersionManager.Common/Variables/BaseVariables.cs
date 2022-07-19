namespace GoLangVersionManager.Common.Variables
{
    public static class BaseVariables
    {
        private static string BASE_PATH => "C:\\ProgramData\\Go Lang Version Manager\\versions";
        private static string GO_BASE_PATH => "{0}\\{1}";
        private static string GO_ROOT_PATH = "{0}\\go";
        private static string GO_ROOT_BIN_PATH = "{0}\\bin";

        public static string GVM_PATH => BASE_PATH;

        public static string GO_BASE_PATH_FORMAT(string goVersion)
            => string.Format(GO_BASE_PATH, BASE_PATH, goVersion);

        public static string GO_ROOT_PATH_FORMAT(string goVersion)
            => string.Format(GO_ROOT_PATH, GO_BASE_PATH_FORMAT(goVersion));

        public static string GO_ROOT_BIN_PATH_FORMAT(string goVersion)
            => string.Format(GO_ROOT_BIN_PATH, GO_ROOT_PATH_FORMAT(goVersion));
    }
}
