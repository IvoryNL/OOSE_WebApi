namespace WebAPI.Handlers.Interfaces
{
    public interface IHandlerAsync<T>
    {
        Task<T> ConsistentieCheckTentamenPlanningHandlerAsync(int id);

        Task<T> ConsistentieCheckCoverageHandlerAsync(int id);
    }
}
