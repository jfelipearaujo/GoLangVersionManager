using GoLangVersionManager.Commands.Verbs;
using GoLangVersionManager.Common.Interfaces;

namespace GoLangVersionManager.Commands.Interfaces
{
    public interface IUseCommand
         : IAppAsyncCommandOption<UseOption>
    {
    }
}
