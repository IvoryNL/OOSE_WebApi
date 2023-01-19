namespace WebAPI.Handlers.Interfaces
{
    public interface IHandlerAsync<T>
    {
        Task<T> GetAsync(int id);
    }
}
