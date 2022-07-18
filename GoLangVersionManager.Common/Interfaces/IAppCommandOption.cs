namespace GoLangVersionManager.Common.Interfaces
{
    public interface IAppCommandOption
    {
        Task<int> RunAsync(IOption option);
    }
}
