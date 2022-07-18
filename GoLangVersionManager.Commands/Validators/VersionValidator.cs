using GoLangVersionManager.Commands.Interfaces;

using System.Text.RegularExpressions;

namespace GoLangVersionManager.Commands.Validators
{
    public class VersionValidator : IVersionValidator
    {
        private const string VERSION_PATTERN = @"^[1-9]\.[1-9][0-9](\.[1-9][0-9]|\.[1-9])?$";

        public bool IsValid(string version)
        {
            return Regex.IsMatch(version, VERSION_PATTERN);
        }
    }
}
