namespace GoLangVersionManager.Common.Interfaces
{
    public interface IAppAsyncCommandOption<TOption>
    {
        Task<int> RunAsync(TOption option);
    }
}
