using GoLangVersionManager.Commands.Interfaces;
using GoLangVersionManager.Common.Variables;

using System.IO.Compression;

namespace GoLangVersionManager.Commands.Helpers
{
    public class UnpackingHelper : IUnpackingHelper
    {
        public void Unpack(string filePath, string goVersion)
        {
            Console.Write("Extracting file... ");

            var destinationPath = BaseVariables.GO_BASE_PATH_FORMAT(goVersion);

            ZipFile.ExtractToDirectory(filePath, destinationPath, true);

            Console.WriteLine("Done!");

            Console.Write("Cleanning up... ");

            File.Delete(filePath);

            Console.WriteLine("Done!");
        }
    }
}
